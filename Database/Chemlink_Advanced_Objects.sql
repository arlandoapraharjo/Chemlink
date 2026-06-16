-- =========================================================================
-- 1. VIEWS
-- =========================================================================

CREATE OR REPLACE VIEW v_detail_produk AS
SELECT 
    p.id_produk, 
    p.nama_produk,
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

-- -------------------------------------------------------------------------

CREATE OR REPLACE VIEW v_ringkasan_pesanan AS
SELECT 
    o.id_order,
    o.no_faktur,
    o.tanggal_order,
    o.keterangan_order,
    u.username AS diinput_oleh
FROM orders o
JOIN Users u ON o.input_by = u.id_user;

-- -------------------------------------------------------------------------

CREATE OR REPLACE VIEW v_stok_kritis AS
SELECT 
    p.id_produk,
    p.nama_produk,
    st.jumlah_stock
FROM Produk p
JOIN Stocks st ON p.id_produk = st.id_produk
WHERE st.jumlah_stock < 10;

-- -------------------------------------------------------------------------

CREATE OR REPLACE VIEW v_laporan_keuangan AS
WITH bulanan AS (
    SELECT 
        TO_CHAR(o.tanggal_order, 'YYYY-MM') AS bulan_group,
        COUNT(DISTINCT o.id_order)::varchar AS total_transaksi,
        SUM(od.jumlah_masuk * p.harga)::varchar AS omzet_bersih
    FROM orders o
    JOIN order_details od ON o.id_order = od.id_order
    JOIN Produk p ON od.id_produk = p.id_produk
    GROUP BY TO_CHAR(o.tanggal_order, 'YYYY-MM')
),
kategori_bulanan AS (
    SELECT 
        TO_CHAR(o.tanggal_order, 'YYYY-MM') AS bulan_group,
        k.nama_kategori,
        SUM(od.jumlah_masuk) AS total_qty,
        ROW_NUMBER() OVER (
            PARTITION BY TO_CHAR(o.tanggal_order, 'YYYY-MM') 
            ORDER BY SUM(od.jumlah_masuk) DESC
        ) AS rn
    FROM orders o
    JOIN order_details od ON o.id_order = od.id_order
    JOIN Produk p ON od.id_produk = p.id_produk
    JOIN Kategori k ON p.id_kategori = k.id_kategori
    GROUP BY TO_CHAR(o.tanggal_order, 'YYYY-MM'), k.nama_kategori
)
SELECT 
    b.bulan_group AS "Bulan",
    b.total_transaksi AS "Total Transaksi",
    COALESCE(kb.nama_kategori, '-') AS "Kategori Terlaris",
    b.omzet_bersih AS "Omzet Bersih"
FROM bulanan b
LEFT JOIN kategori_bulanan kb 
    ON b.bulan_group = kb.bulan_group AND kb.rn = 1
ORDER BY b.bulan_group DESC;

-- -------------------------------------------------------------------------

CREATE OR REPLACE VIEW v_show_supplier AS
SELECT id_supplier, nama_perusahaan, no_telp, kota_supplier 
FROM Supplier 
ORDER BY id_supplier ASC;

-- -------------------------------------------------------------------------

CREATE OR REPLACE VIEW v_ringkasan_selling AS
SELECT 
    sl.id_selling,
    sl.no_faktur,
    sl.tanggal_selling,
    sl.keterangan,
    u.username AS kasir
FROM selling sl
JOIN Users u ON sl.id_kasir = u.id_user;

-- -------------------------------------------------------------------------

CREATE OR REPLACE VIEW v_log_stok AS
SELECT
    id_log,
    tipe_aktivitas,
    id_user,
    nama_user,
    nama_produk,
    jumlah,
    time_stamp
FROM log_stok
ORDER BY time_stamp DESC;

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

-- -------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION fn_hitung_total_selling(p_id_selling INT)
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

-- -------------------------------------------------------------------------

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

-- -------------------------------------------------------------------------

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
    p_harga INTEGER,
    p_tanggal_exp DATE,
    p_keterangan TEXT,
    p_id_kategori INTEGER,
    p_id_supplier INTEGER
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_id_produk INTEGER;
BEGIN
    INSERT INTO Produk (nama_produk, harga, tanggal_exp, keterangan, id_kategori, id_supplier)
    VALUES (p_nama_produk, p_harga, p_tanggal_exp, p_keterangan, p_id_kategori, p_id_supplier)
    RETURNING id_produk INTO v_id_produk;

    INSERT INTO Stocks (jumlah_stock, id_produk)
    VALUES (0, v_id_produk);
END;
$$;

-- -------------------------------------------------------------------------

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
END;
$$;

-- -------------------------------------------------------------------------

CREATE OR REPLACE PROCEDURE sp_tambah_user(
    p_username VARCHAR,
    p_password VARCHAR,
    p_role VARCHAR,
    p_fullname VARCHAR,
    p_alamat TEXT,
    p_kecamatan VARCHAR,
    p_no_telp VARCHAR,
    p_email VARCHAR
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO Users (username, password, Role, fullname, alamat, kecamatan, no_telp, email, status)
    VALUES (p_username, p_password, p_role, p_fullname, p_alamat, p_kecamatan, p_no_telp, p_email, 'Active');
END;
$$;

-- -------------------------------------------------------------------------

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
END;
$$;

-- -------------------------------------------------------------------------

CREATE OR REPLACE PROCEDURE sp_catat_pesanan_masuk(
    p_no_faktur VARCHAR,
    p_tanggal_order DATE,
    p_keterangan TEXT,
    p_input_by INTEGER,
    p_id_produk INTEGER,
    p_jumlah_masuk INTEGER,
    p_catatan_detail TEXT
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_id_order INTEGER;
BEGIN
    INSERT INTO orders (no_faktur, tanggal_order, keterangan_order, input_by)
    VALUES (p_no_faktur, p_tanggal_order, p_keterangan, p_input_by)
    RETURNING id_order INTO v_id_order;

    -- Trigger trg_log_stok_masuk otomatis jalan setelah INSERT ini
    INSERT INTO order_details (jumlah_masuk, catatan, id_order, id_produk)
    VALUES (p_jumlah_masuk, p_catatan_detail, v_id_order, p_id_produk);
END;
$$;

-- -------------------------------------------------------------------------

CREATE OR REPLACE PROCEDURE sp_catat_selling(
    p_no_faktur VARCHAR,
    p_tanggal_selling DATE,
    p_keterangan TEXT,
    p_id_kasir INTEGER,
    p_id_produk INTEGER,
    p_jumlah_keluar INTEGER,
    p_catatan_detail TEXT
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_id_selling INTEGER;
BEGIN
    INSERT INTO selling (no_faktur, tanggal_selling, keterangan, id_kasir)
    VALUES (p_no_faktur, p_tanggal_selling, p_keterangan, p_id_kasir)
    RETURNING id_selling INTO v_id_selling;

    -- Trigger trg_log_stok_keluar otomatis jalan setelah INSERT ini
    INSERT INTO selling_details (jumlah_keluar, catatan, id_selling, id_produk)
    VALUES (p_jumlah_keluar, p_catatan_detail, v_id_selling, p_id_produk);
END;
$$;

-- =========================================================================
-- 4. TRANSACTIONS
-- =========================================================================

CREATE OR REPLACE PROCEDURE sp_transaksi_pesanan_masuk(
    p_no_faktur VARCHAR,
    p_tanggal_order DATE,
    p_keterangan TEXT,
    p_input_by INTEGER,
    p_id_produk INTEGER,
    p_jumlah_masuk INTEGER,
    p_catatan_detail TEXT
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_id_order INTEGER;
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Produk WHERE id_produk = p_id_produk) THEN
        RAISE EXCEPTION 'Produk dengan id % tidak ditemukan.', p_id_produk;
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Users WHERE id_user = p_input_by) THEN
        RAISE EXCEPTION 'User dengan id % tidak ditemukan.', p_input_by;
    END IF;

    IF p_jumlah_masuk <= 0 THEN
        RAISE EXCEPTION 'Jumlah masuk harus lebih dari 0.';
    END IF;

    INSERT INTO orders (no_faktur, tanggal_order, keterangan_order, input_by)
    VALUES (p_no_faktur, p_tanggal_order, p_keterangan, p_input_by)
    RETURNING id_order INTO v_id_order;

    -- Trigger trg_log_stok_masuk otomatis jalan setelah INSERT ini
    INSERT INTO order_details (jumlah_masuk, catatan, id_order, id_produk)
    VALUES (p_jumlah_masuk, p_catatan_detail, v_id_order, p_id_produk);

    RAISE NOTICE 'Transaksi masuk berhasil. ID Order: %, Stok produk % bertambah % unit.',
        v_id_order, p_id_produk, p_jumlah_masuk;

EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Transaksi gagal dan di-rollback. Error: %', SQLERRM;
END;
$$;

-- -------------------------------------------------------------------------

CREATE OR REPLACE PROCEDURE sp_transaksi_selling(
    p_no_faktur VARCHAR,
    p_tanggal_selling DATE,
    p_keterangan TEXT,
    p_id_kasir INTEGER,
    p_id_produk INTEGER,
    p_jumlah_keluar INTEGER,
    p_catatan_detail TEXT
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_id_selling INTEGER;
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Produk WHERE id_produk = p_id_produk) THEN
        RAISE EXCEPTION 'Produk dengan id % tidak ditemukan.', p_id_produk;
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Users WHERE id_user = p_id_kasir AND Role IN ('Kasir', 'Admin') AND status = 'Active') THEN
        RAISE EXCEPTION 'Kasir/Admin dengan id % tidak ditemukan.', p_id_kasir;
    END IF;

    IF p_jumlah_keluar <= 0 THEN
        RAISE EXCEPTION 'Jumlah keluar harus lebih dari 0.';
    END IF;

    IF NOT fn_cek_ketersediaan_stok(p_id_produk, p_jumlah_keluar) THEN
        RAISE EXCEPTION 'Stok produk id % tidak mencukupi.', p_id_produk;
    END IF;

    INSERT INTO selling (no_faktur, tanggal_selling, keterangan, id_kasir)
    VALUES (p_no_faktur, p_tanggal_selling, p_keterangan, p_id_kasir)
    RETURNING id_selling INTO v_id_selling;

    -- Trigger trg_log_stok_keluar otomatis jalan setelah INSERT ini
    INSERT INTO selling_details (jumlah_keluar, catatan, id_selling, id_produk)
    VALUES (p_jumlah_keluar, p_catatan_detail, v_id_selling, p_id_produk);

    RAISE NOTICE 'Transaksi selling berhasil. ID Selling: %, Stok produk % berkurang % unit.',
        v_id_selling, p_id_produk, p_jumlah_keluar;

EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Transaksi gagal dan di-rollback. Error: %', SQLERRM;
END;
$$;

-- =========================================================================
-- 5. TRIGGERS
-- =========================================================================

CREATE OR REPLACE FUNCTION fn_trigger_log_stok_masuk()
RETURNS TRIGGER AS $$
DECLARE
    v_id_user INTEGER;
    v_nama_user VARCHAR(255);
    v_nama_produk VARCHAR(150);
BEGIN
    SELECT o.input_by, u.fullname
    INTO v_id_user, v_nama_user
    FROM orders o
    JOIN Users u ON o.input_by = u.id_user
    WHERE o.id_order = NEW.id_order;

    SELECT nama_produk INTO v_nama_produk
    FROM Produk
    WHERE id_produk = NEW.id_produk;

    UPDATE Stocks
    SET jumlah_stock = jumlah_stock + NEW.jumlah_masuk,
        time_stamp = CURRENT_TIMESTAMP
    WHERE id_produk = NEW.id_produk;

    INSERT INTO log_stok (tipe_aktivitas, id_user, nama_user, nama_produk, jumlah)
    VALUES ('MASUK', v_id_user, v_nama_user, v_nama_produk, NEW.jumlah_masuk);

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE TRIGGER trg_log_stok_masuk
AFTER INSERT ON order_details
FOR EACH ROW
EXECUTE FUNCTION fn_trigger_log_stok_masuk();

-- -------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION fn_trigger_log_stok_keluar()
RETURNS TRIGGER AS $$
DECLARE
    v_id_kasir INTEGER;
    v_nama_kasir VARCHAR(255);
    v_nama_produk VARCHAR(150);
BEGIN
    SELECT sl.id_kasir, u.fullname
    INTO v_id_kasir, v_nama_kasir
    FROM selling sl
    JOIN Users u ON sl.id_kasir = u.id_user
    WHERE sl.id_selling = NEW.id_selling;

    SELECT nama_produk INTO v_nama_produk
    FROM Produk
    WHERE id_produk = NEW.id_produk;

    UPDATE Stocks
    SET jumlah_stock = jumlah_stock - NEW.jumlah_keluar,
        time_stamp = CURRENT_TIMESTAMP
    WHERE id_produk = NEW.id_produk;

    INSERT INTO log_stok (tipe_aktivitas, id_user, nama_user, nama_produk, jumlah)
    VALUES ('KELUAR', v_id_kasir, v_nama_kasir, v_nama_produk, NEW.jumlah_keluar);

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE TRIGGER trg_log_stok_keluar
AFTER INSERT ON selling_details
FOR EACH ROW
EXECUTE FUNCTION fn_trigger_log_stok_keluar();