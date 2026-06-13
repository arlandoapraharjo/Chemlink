CREATE OR REPLACE VIEW v_detail_produk AS
SELECT 
    p.id_produk, 
    p.nama_produk, 
    p.merek, 
    p.satuan, 
    p.harga, 
    p.tanggal_exp, 
    p.keterangan,
    k.nama_kategori,
    s.nama_perusahaan AS nama_supplier,
    st.jumlah_stock
FROM Produk p
JOIN Kategori k ON p.id_kategori = k.id_kategori
JOIN Supplier s ON p.id_supplier = s.id_supplier
LEFT JOIN Stocks st ON p.id_produk = st.id_produk;

CREATE OR REPLACE VIEW v_ringkasan_pesanan AS
SELECT 
    o.id_order,
    o.tanggal,
    o.jumlah AS total_item_pesanan,
    o.keterangan_order,
    u.username AS diinput_oleh
FROM orders o
JOIN Users u ON o.input_by = u.id_user;

CREATE OR REPLACE VIEW v_stok_kritis AS
SELECT 
    p.id_produk,
    p.nama_produk,
    st.jumlah_stock
FROM Produk p
JOIN Stocks st ON p.id_produk = st.id_produk
WHERE st.jumlah_stock < 10;

CREATE OR REPLACE VIEW v_laporan_keuangan AS
WITH bulanan AS (
    SELECT 
        TO_CHAR(o.tanggal, 'YYYY-MM') AS bulan_group,
        COUNT(DISTINCT o.id_order)::varchar AS total_transaksi,
        SUM(od.jumlah_masuk * p.harga)::varchar AS omzet_bersih
    FROM orders o
    JOIN order_details od ON o.id_order = od.id_order
    JOIN Produk p ON od.id_produk = p.id_produk
    GROUP BY TO_CHAR(o.tanggal, 'YYYY-MM')
),
kategori_bulanan AS (
    SELECT 
        TO_CHAR(o.tanggal, 'YYYY-MM') AS bulan_group,
        k.nama_kategori,
        SUM(od.jumlah_masuk) AS total_qty,
        ROW_NUMBER() OVER (PARTITION BY TO_CHAR(o.tanggal, 'YYYY-MM') ORDER BY SUM(od.jumlah_masuk) DESC) as rn
    FROM orders o
    JOIN order_details od ON o.id_order = od.id_order
    JOIN Produk p ON od.id_produk = p.id_produk
    JOIN Kategori k ON p.id_kategori = k.id_kategori
    GROUP BY TO_CHAR(o.tanggal, 'YYYY-MM'), k.nama_kategori
)
SELECT 
    b.bulan_group AS "Bulan",
    b.total_transaksi AS "Total Transaksi",
    COALESCE(kb.nama_kategori, '-') AS "Kategori Terlaris",
    b.omzet_bersih AS "Omzet Bersih"
FROM bulanan b
LEFT JOIN kategori_bulanan kb ON b.bulan_group = kb.bulan_group AND kb.rn = 1
ORDER BY b.bulan_group DESC;

-- =========================================================================
-- 2. FUNCTIONS
-- =========================================================================

CREATE OR REPLACE FUNCTION fn_hitung_total_pesanan(p_id_order INT)
RETURNS INTEGER AS $$
DECLARE
    total_jumlah INTEGER;
BEGIN
    SELECT COALESCE(SUM(jumlah_masuk), 0) INTO total_jumlah
    FROM order_details
    WHERE id_order = p_id_order;
    
    RETURN total_jumlah;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION fn_cek_ketersediaan_stok(p_id_produk INT, p_jumlah_diminta INT)
RETURNS BOOLEAN AS $$
DECLARE
    stok_sekarang INTEGER;
BEGIN
    SELECT jumlah_stock INTO stok_sekarang
    FROM Stocks
    WHERE id_produk = p_id_produk;
    
    IF stok_sekarang >= p_jumlah_diminta THEN
        RETURN TRUE;
    ELSE
        RETURN FALSE;
    END IF;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION fn_get_harga_produk(p_id_produk INT)
RETURNS INTEGER AS $$
DECLARE
    v_harga INTEGER;
BEGIN
    SELECT harga INTO v_harga
    FROM Produk
    WHERE id_produk = p_id_produk;
    
    RETURN v_harga;
END;
$$ LANGUAGE plpgsql;

-- =========================================================================
-- 3. STORED PROCEDURES
-- =========================================================================

CREATE OR REPLACE PROCEDURE sp_tambah_produk_baru(
    p_nama_produk VARCHAR,
    p_merek VARCHAR,
    p_satuan VARCHAR,
    p_harga INTEGER,
    p_tanggal_exp DATE,
    p_keterangan TEXT,
    p_id_kategori INTEGER,
    p_id_supplier INTEGER,
    p_id_user INTEGER
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_id_produk INTEGER;
BEGIN
    INSERT INTO Produk (nama_produk, merek, satuan, harga, tanggal_exp, keterangan, id_kategori, id_supplier, id_user)
    VALUES (p_nama_produk, p_merek, p_satuan, p_harga, p_tanggal_exp, p_keterangan, p_id_kategori, p_id_supplier, p_id_user)
    RETURNING id_produk INTO v_id_produk;

    INSERT INTO Stocks (jumlah_stock, id_produk)
    VALUES (0, v_id_produk);
    
    COMMIT;
END;
$$;

CREATE OR REPLACE PROCEDURE sp_tambah_supplier(
    p_nama_perusahaan VARCHAR,
    p_kontak_person VARCHAR,
    p_no_telp VARCHAR,
    p_email VARCHAR,
    p_alamat_supplier TEXT,
    p_kota_supplier VARCHAR
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO Supplier (nama_perusahaan, kontak_person, no_telp, email, alamat_supplier, kota_supplier, status)
    VALUES (p_nama_perusahaan, p_kontak_person, p_no_telp, p_email, p_alamat_supplier, p_kota_supplier, 'Active');
    COMMIT;
END;
$$;

CREATE OR REPLACE PROCEDURE sp_tambah_user(
    p_username VARCHAR,
    p_password VARCHAR,
    p_role VARCHAR
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO Users (username, password, Role, status)
    VALUES (p_username, p_password, p_role, 'Active');
    COMMIT;
END;
$$;

CREATE OR REPLACE PROCEDURE sp_update_status_user(
    p_id_user INTEGER,
    p_status VARCHAR
)
LANGUAGE plpgsql
AS $$
BEGIN
    UPDATE Users
    SET status = p_status
    WHERE id_user = p_id_user;
    
    COMMIT;
END;
$$;

CREATE OR REPLACE PROCEDURE sp_catat_pesanan_masuk(
    p_tanggal DATE,
    p_keterangan TEXT,
    p_input_by INTEGER,
    p_id_produk INTEGER,
    p_jumlah_masuk INTEGER,
    p_no_faktur VARCHAR,
    p_catatan_detail TEXT
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_id_order INTEGER;
BEGIN
    INSERT INTO orders (tanggal, jumlah, keterangan_order, input_by)
    VALUES (p_tanggal, 1, p_keterangan, p_input_by)
    RETURNING id_order INTO v_id_order;

    INSERT INTO order_details (tanggal, no_faktur, jumlah_masuk, catatan, id_order, id_produk)
    VALUES (p_tanggal, p_no_faktur, p_jumlah_masuk, p_catatan_detail, v_id_order, p_id_produk);

    UPDATE Stocks 
    SET jumlah_stock = jumlah_stock - p_jumlah_masuk,
        timestamp = CURRENT_TIMESTAMP
    WHERE id_produk = p_id_produk;
END;
$$;

CREATE OR REPLACE VIEW v_show_supplier AS
SELECT id_supplier, nama_perusahaan, no_telp, kota_supplier FROM Supplier ORDER BY id_supplier ASC;
