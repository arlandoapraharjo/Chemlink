-- Use chemlink_sch schema
SET search_path TO chemlink_sch, public;

-- Drop views first (CREATE OR REPLACE cannot drop columns)
DROP VIEW IF EXISTS v_detail_produk CASCADE;
DROP VIEW IF EXISTS v_stok_kritis CASCADE;
DROP VIEW IF EXISTS v_laporan_keuangan CASCADE;
DROP VIEW IF EXISTS v_show_supplier CASCADE;
DROP VIEW IF EXISTS v_log_stok CASCADE;

CREATE OR REPLACE VIEW v_detail_produk AS
SELECT 
    p.id_produk, 
    p.nama_produk, 
    p.harga, 
    p.keterangan,
    k.nama_kategori,
    s.nama_perusahaan AS nama_supplier,
    st.jumlah_stock
FROM Produk p
JOIN Kategori k ON p.id_kategori = k.id_kategori
JOIN Supplier s ON p.id_supplier = s.id_supplier
LEFT JOIN Stocks st ON p.id_produk = st.id_produk;

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
        TO_CHAR(s.tanggal_selling, 'YYYY-MM') AS bulan_group,
        COUNT(DISTINCT s.id_selling)::varchar AS total_transaksi,
        SUM(sd.jumlah_keluar * p.harga)::varchar AS omzet_bersih
    FROM selling s
    JOIN selling_details sd ON s.id_selling = sd.id_selling
    JOIN Produk p ON sd.id_produk = p.id_produk
    GROUP BY TO_CHAR(s.tanggal_selling, 'YYYY-MM')
),
kategori_bulanan AS (
    SELECT 
        TO_CHAR(s.tanggal_selling, 'YYYY-MM') AS bulan_group,
        k.nama_kategori,
        SUM(sd.jumlah_keluar) AS total_qty,
        ROW_NUMBER() OVER (PARTITION BY TO_CHAR(s.tanggal_selling, 'YYYY-MM') ORDER BY SUM(sd.jumlah_keluar) DESC) as rn
    FROM selling s
    JOIN selling_details sd ON s.id_selling = sd.id_selling
    JOIN Produk p ON sd.id_produk = p.id_produk
    JOIN Kategori k ON p.id_kategori = k.id_kategori
    GROUP BY TO_CHAR(s.tanggal_selling, 'YYYY-MM'), k.nama_kategori
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

CREATE OR REPLACE FUNCTION fn_hitung_total_pesanan(p_id_selling INT)
RETURNS INTEGER AS $$
DECLARE
    total_jumlah INTEGER;
BEGIN
    SELECT COALESCE(SUM(jumlah_keluar), 0) INTO total_jumlah
    FROM selling_details
    WHERE id_selling = p_id_selling;
    
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

-- Drop procedures with changed signatures (CREATE OR REPLACE can't change param count)
DROP PROCEDURE IF EXISTS sp_tambah_produk_baru(VARCHAR, INTEGER, DATE, TEXT, INTEGER, INTEGER, INTEGER);
DROP PROCEDURE IF EXISTS sp_tambah_produk_baru(VARCHAR, VARCHAR, VARCHAR, INTEGER, DATE, TEXT, INTEGER, INTEGER, INTEGER);
DROP PROCEDURE IF EXISTS sp_transaksi_selling(VARCHAR, DATE, TEXT, INTEGER, INTEGER, INTEGER, TEXT);
DROP PROCEDURE IF EXISTS sp_catat_pesanan_masuk(DATE, TEXT, INTEGER, INTEGER, INTEGER, VARCHAR, TEXT);

CREATE OR REPLACE PROCEDURE sp_tambah_produk_baru(
    p_nama_produk VARCHAR,
    p_harga INTEGER,
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
    INSERT INTO Produk (nama_produk, harga, keterangan, id_kategori, id_supplier, id_user)
    VALUES (p_nama_produk, p_harga, p_keterangan, p_id_kategori, p_id_supplier, p_id_user)
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
    p_role VARCHAR,
    p_fullname VARCHAR DEFAULT NULL,
    p_alamat TEXT DEFAULT NULL,
    p_kecamatan VARCHAR DEFAULT NULL,
    p_no_telp VARCHAR DEFAULT NULL,
    p_email VARCHAR DEFAULT NULL
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO Users (username, password, Role, fullname, status, alamat, kecamatan, no_telp, email)
    VALUES (p_username, p_password, p_role, COALESCE(p_fullname, p_username), 'Active', p_alamat, p_kecamatan, p_no_telp, p_email);
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

CREATE OR REPLACE PROCEDURE sp_transaksi_selling(
    p_tanggal_selling DATE,
    p_keterangan TEXT,
    p_id_kasir INTEGER,
    p_id_produk INTEGER,
    p_jumlah_keluar INTEGER
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_id_selling INTEGER;
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Produk WHERE id_produk = p_id_produk) THEN
        RAISE EXCEPTION 'Produk dengan id % tidak ditemukan.', p_id_produk;
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Users WHERE id_user = p_id_kasir AND role IN ('Kasir', 'Admin') AND status = 'Active') THEN
        RAISE EXCEPTION 'Kasir/Admin dengan id % tidak ditemukan.', p_id_kasir;
    END IF;

    IF p_jumlah_keluar <= 0 THEN
        RAISE EXCEPTION 'Jumlah keluar harus lebih dari 0.';
    END IF;

    IF NOT fn_cek_ketersediaan_stok(p_id_produk, p_jumlah_keluar) THEN
        RAISE EXCEPTION 'Stok produk id % tidak mencukupi.', p_id_produk;
    END IF;

    -- Insert selling header
    INSERT INTO selling (tanggal_selling, keterangan, id_kasir)
    VALUES (p_tanggal_selling, p_keterangan, p_id_kasir)
    RETURNING id_selling INTO v_id_selling;

    -- Insert selling detail (triggers auto-decrement stock + auto-log)
    INSERT INTO selling_details (jumlah_keluar, id_selling, id_produk)
    VALUES (p_jumlah_keluar, v_id_selling, p_id_produk);
END;
$$;

CREATE OR REPLACE VIEW v_show_supplier AS
SELECT id_supplier, nama_perusahaan, no_telp, kota_supplier FROM Supplier ORDER BY id_supplier ASC;

CREATE OR REPLACE VIEW v_log_stok AS
SELECT 
    ls.id_log,
    ls.tipe_aktivitas,
    ls.nama_user,
    ls.nama_produk,
    ls.jumlah,
    ls.time_stamp
FROM log_stok ls
ORDER BY ls.time_stamp DESC;
