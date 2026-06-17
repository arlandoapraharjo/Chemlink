-- Use chemlink_sch schema
SET search_path TO chemlink_sch, public;

-- =========================================================================
-- 1. VIEWS
-- =========================================================================

-- Drop views first (CREATE OR REPLACE cannot drop columns)
DROP VIEW IF EXISTS v_detail_produk CASCADE;
DROP VIEW IF EXISTS v_stok_kritis CASCADE;
DROP VIEW IF EXISTS v_laporan_keuangan CASCADE;
DROP VIEW IF EXISTS v_show_supplier CASCADE;
DROP VIEW IF EXISTS v_log_stok CASCADE;
DROP VIEW IF EXISTS v_user_aktif CASCADE;
DROP VIEW IF EXISTS v_supplier CASCADE;
DROP VIEW IF EXISTS v_kategori CASCADE;
DROP VIEW IF EXISTS v_penjualan_per_kategori CASCADE;

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

-- Active users (excludes soft-deleted Inactive users)
CREATE OR REPLACE VIEW v_user_aktif AS
SELECT id_user, username, password, role, fullname, status,
       alamat, no_telp, email, kecamatan
FROM Users
WHERE status = 'Active'
ORDER BY id_user ASC;

-- Full supplier listing
CREATE OR REPLACE VIEW v_supplier AS
SELECT id_supplier, nama_perusahaan, kontak_person, no_telp, email,
       alamat_supplier, kota_supplier, status
FROM Supplier
ORDER BY id_supplier ASC;

-- Category listing
CREATE OR REPLACE VIEW v_kategori AS
SELECT id_kategori, nama_kategori
FROM Kategori
ORDER BY id_kategori ASC;

-- Sales breakdown per category
CREATE OR REPLACE VIEW v_penjualan_per_kategori AS
SELECT 
    k.nama_kategori AS "Kategori",
    SUM(sd.jumlah_keluar)::int AS "Qty Terjual",
    (SUM(sd.jumlah_keluar * p.harga))::int AS "Total Pendapatan"
FROM selling_details sd
JOIN Produk p ON sd.id_produk = p.id_produk
JOIN Kategori k ON p.id_kategori = k.id_kategori
GROUP BY k.nama_kategori
ORDER BY SUM(sd.jumlah_keluar * p.harga) DESC;

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
    
    RETURN stok_sekarang >= p_jumlah_diminta;
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

-- Authenticate user by username (only active users).
-- Password verification is done in application code.
DROP FUNCTION IF EXISTS fn_autentikasi_user(VARCHAR) CASCADE;
DROP FUNCTION IF EXISTS fn_autentikasi_user(TEXT) CASCADE;
CREATE OR REPLACE FUNCTION fn_autentikasi_user(p_username TEXT)
RETURNS TABLE (
    id_user INTEGER,
    username TEXT,
    password TEXT,
    role TEXT,
    fullname TEXT,
    status TEXT,
    alamat TEXT,
    no_telp TEXT,
    email TEXT,
    kecamatan TEXT
) AS $$
BEGIN
    RETURN QUERY
    SELECT u.id_user, u.username::TEXT, u.password::TEXT, u.role::TEXT, u.fullname::TEXT,
           u.status::TEXT, u.alamat, u.no_telp::TEXT, u.email::TEXT, u.kecamatan::TEXT
    FROM Users u
    WHERE u.username = p_username AND u.status = 'Active';
END;
$$ LANGUAGE plpgsql;

-- =========================================================================
-- 3. STORED PROCEDURES
-- =========================================================================

-- Drop all sp_ procedures and functions (handles overloaded names)
DO $$
DECLARE
    r RECORD;
BEGIN
    FOR r IN
        SELECT p.oid::regprocedure AS sig, p.prokind
        FROM pg_proc p
        JOIN pg_namespace n ON p.pronamespace = n.oid
        WHERE n.nspname = 'public'
          AND (p.proname LIKE 'sp_%' OR p.proname = 'fn_hapus_user')
    LOOP
        IF r.prokind = 'f' THEN
            EXECUTE 'DROP FUNCTION IF EXISTS ' || r.sig || ' CASCADE';
        ELSE
            EXECUTE 'DROP PROCEDURE IF EXISTS ' || r.sig || ' CASCADE';
        END IF;
    END LOOP;
END;
$$;

-- ── Product ────────────────────────────────────────────────────────────

-- Create a new product with initial stock.
-- Stocks INSERT triggers fn_trg_stocks_update which auto-logs 'IN'.
CREATE OR REPLACE PROCEDURE sp_tambah_produk_baru(
    p_nama_produk TEXT,
    p_harga INTEGER,
    p_keterangan TEXT,
    p_id_kategori INTEGER,
    p_id_supplier INTEGER,
    p_id_user INTEGER,
    p_stok_awal INTEGER DEFAULT 0
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
    VALUES (p_stok_awal, v_id_produk);

    COMMIT;
END;
$$;

-- Update product details and stock in one call.
-- Produk update triggers fn_trg_produk_name_change for name cascade.
CREATE OR REPLACE PROCEDURE sp_update_produk(
    p_id_produk INTEGER,
    p_nama_produk TEXT,
    p_id_kategori INTEGER,
    p_harga INTEGER,
    p_keterangan TEXT,
    p_stok INTEGER,
    p_id_supplier INTEGER
)
LANGUAGE plpgsql
AS $$
BEGIN
    UPDATE Produk
    SET nama_produk = p_nama_produk,
        id_kategori = p_id_kategori,
        harga = p_harga,
        keterangan = p_keterangan,
        id_supplier = p_id_supplier
    WHERE id_produk = p_id_produk;

    UPDATE Stocks
    SET jumlah_stock = p_stok,
        timestamp = CURRENT_TIMESTAMP
    WHERE id_produk = p_id_produk;

    COMMIT;
END;
$$;

-- Delete a product and its stock row (respects FK constraints).
CREATE OR REPLACE PROCEDURE sp_hapus_produk(
    p_id_produk INTEGER
)
LANGUAGE plpgsql
AS $$
BEGIN
    DELETE FROM Stocks WHERE id_produk = p_id_produk;
    DELETE FROM Produk WHERE id_produk = p_id_produk;
    COMMIT;
END;
$$;

-- ── Sales / Checkout ──────────────────────────────────────────────────

-- Process a complete checkout: header + all detail lines.
-- Each selling_details INSERT fires fn_trg_selling_detail
-- which auto-decrements stock and auto-logs 'OUT'.
CREATE OR REPLACE PROCEDURE sp_checkout(
    p_tanggal DATE,
    p_keterangan TEXT,
    p_id_kasir INTEGER,
    p_id_produk INTEGER[],
    p_jumlah INTEGER[]
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_id_selling INTEGER;
    i INTEGER;
BEGIN
    IF array_length(p_id_produk, 1) IS NULL OR
       array_length(p_id_produk, 1) != array_length(p_jumlah, 1) THEN
        RAISE EXCEPTION 'Array produk dan jumlah harus memiliki panjang yang sama.';
    END IF;

    INSERT INTO selling (tanggal_selling, keterangan, id_kasir)
    VALUES (p_tanggal, p_keterangan, p_id_kasir)
    RETURNING id_selling INTO v_id_selling;

    FOR i IN 1..array_length(p_id_produk, 1) LOOP
        INSERT INTO selling_details (jumlah_keluar, id_selling, id_produk)
        VALUES (p_jumlah[i], v_id_selling, p_id_produk[i]);
    END LOOP;

    COMMIT;
END;
$$;

-- ── User Management ───────────────────────────────────────────────────

-- Add user: reactivates an inactive user with the same username,
-- or inserts a new one.
CREATE OR REPLACE PROCEDURE sp_tambah_user(
    p_username TEXT,
    p_password TEXT,
    p_role TEXT,
    p_fullname TEXT DEFAULT NULL,
    p_alamat TEXT DEFAULT NULL,
    p_kecamatan TEXT DEFAULT NULL,
    p_no_telp TEXT DEFAULT NULL,
    p_email TEXT DEFAULT NULL
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_existing_id INTEGER;
    v_id_user INTEGER;
BEGIN
    -- Check if an inactive user with the same username exists
    SELECT id_user INTO v_existing_id
    FROM Users
    WHERE username = p_username AND status = 'Inactive'
    LIMIT 1;

    IF v_existing_id IS NOT NULL THEN
        -- Reactivate the soft-deleted user with new data
        UPDATE Users
        SET password = p_password,
            role = p_role,
            fullname = COALESCE(p_fullname, p_username),
            status = 'Active',
            alamat = p_alamat,
            no_telp = p_no_telp,
            email = p_email,
            kecamatan = p_kecamatan
        WHERE id_user = v_existing_id;

        v_id_user := v_existing_id;
    ELSE
        -- Insert as new user
        INSERT INTO Users (username, password, role, fullname, status,
                           alamat, kecamatan, no_telp, email)
        VALUES (p_username, p_password, p_role,
                COALESCE(p_fullname, p_username), 'Active',
                p_alamat, p_kecamatan, p_no_telp, p_email)
        RETURNING id_user INTO v_id_user;
    END IF;

    COMMIT;
END;
$$;

-- Update user (with optional password change).
CREATE OR REPLACE PROCEDURE sp_update_user(
    p_id_user INTEGER,
    p_username TEXT,
    p_password TEXT,
    p_role TEXT,
    p_fullname TEXT,
    p_status TEXT,
    p_alamat TEXT,
    p_no_telp TEXT,
    p_email TEXT,
    p_kecamatan TEXT
)
LANGUAGE plpgsql
AS $$
BEGIN
    IF p_password IS NOT NULL AND p_password != '' THEN
        UPDATE Users
        SET username = p_username,
            password = p_password,
            role = p_role,
            fullname = p_fullname,
            status = p_status,
            alamat = p_alamat,
            no_telp = p_no_telp,
            email = p_email,
            kecamatan = p_kecamatan
        WHERE id_user = p_id_user;
    ELSE
        UPDATE Users
        SET username = p_username,
            role = p_role,
            fullname = p_fullname,
            status = p_status,
            alamat = p_alamat,
            no_telp = p_no_telp,
            email = p_email,
            kecamatan = p_kecamatan
        WHERE id_user = p_id_user;
    END IF;

    COMMIT;
END;
$$;

-- Soft-delete user. Returns TRUE if deleted, FALSE if it would remove
-- the last active admin (admin protection).
CREATE OR REPLACE FUNCTION fn_hapus_user(
    p_id_user INTEGER
)
RETURNS BOOLEAN
LANGUAGE plpgsql
AS $$
DECLARE
    v_role TEXT;
    v_admin_count INTEGER;
BEGIN
    -- Check if this user is an admin
    SELECT role INTO v_role FROM Users WHERE id_user = p_id_user;

    IF v_role = 'Admin' THEN
        SELECT COUNT(*) INTO v_admin_count
        FROM Users
        WHERE role = 'Admin' AND status = 'Active';

        IF v_admin_count <= 1 THEN
            RETURN FALSE;
        END IF;
    END IF;

    UPDATE Users SET status = 'Inactive' WHERE id_user = p_id_user;
    RETURN TRUE;
END;
$$;

-- ── Category ──────────────────────────────────────────────────────────

CREATE OR REPLACE PROCEDURE sp_kategori_create(
    p_nama TEXT
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_id_kategori INTEGER;
BEGIN
    INSERT INTO Kategori (nama_kategori)
    VALUES (p_nama)
    RETURNING id_kategori INTO v_id_kategori;

    COMMIT;
END;
$$;

CREATE OR REPLACE PROCEDURE sp_kategori_update(
    p_id_kategori INTEGER,
    p_nama TEXT
)
LANGUAGE plpgsql
AS $$
BEGIN
    UPDATE Kategori SET nama_kategori = p_nama WHERE id_kategori = p_id_kategori;
    COMMIT;
END;
$$;

CREATE OR REPLACE PROCEDURE sp_kategori_delete(
    p_id_kategori INTEGER
)
LANGUAGE plpgsql
AS $$
BEGIN
    DELETE FROM Kategori WHERE id_kategori = p_id_kategori;
    COMMIT;
END;
$$;

-- ── Supplier ──────────────────────────────────────────────────────────

CREATE OR REPLACE PROCEDURE sp_tambah_supplier(
    p_nama_perusahaan TEXT,
    p_kontak_person TEXT,
    p_no_telp TEXT,
    p_email TEXT,
    p_alamat_supplier TEXT,
    p_kota_supplier TEXT
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO Supplier (nama_perusahaan, kontak_person, no_telp, email,
                          alamat_supplier, kota_supplier, status)
    VALUES (p_nama_perusahaan, p_kontak_person, p_no_telp, p_email,
            p_alamat_supplier, p_kota_supplier, 'Active');
    COMMIT;
END;
$$;

CREATE OR REPLACE PROCEDURE sp_supplier_update(
    p_id_supplier INTEGER,
    p_nama_perusahaan TEXT,
    p_kontak_person TEXT,
    p_no_telp TEXT,
    p_email TEXT,
    p_alamat_supplier TEXT,
    p_kota_supplier TEXT,
    p_status TEXT
)
LANGUAGE plpgsql
AS $$
BEGIN
    UPDATE Supplier
    SET nama_perusahaan = p_nama_perusahaan,
        kontak_person = p_kontak_person,
        no_telp = p_no_telp,
        email = p_email,
        alamat_supplier = p_alamat_supplier,
        kota_supplier = p_kota_supplier,
        status = p_status
    WHERE id_supplier = p_id_supplier;

    COMMIT;
END;
$$;

CREATE OR REPLACE PROCEDURE sp_supplier_delete(
    p_id_supplier INTEGER
)
LANGUAGE plpgsql
AS $$
BEGIN
    DELETE FROM Supplier WHERE id_supplier = p_id_supplier;
    COMMIT;
END;
$$;

-- Keep sp_transaksi_selling for direct stored-procedure-based single-item sales
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

-- =========================================================================
-- 4. TRIGGERS
-- =========================================================================

-- Drop existing triggers for idempotent re-runs
DROP TRIGGER IF EXISTS trg_selling_detail_after_insert ON selling_details;
DROP TRIGGER IF EXISTS trg_stocks_after_update ON Stocks;
DROP TRIGGER IF EXISTS trg_produk_name_change ON Produk;
DROP FUNCTION IF EXISTS fn_trg_selling_detail() CASCADE;
DROP FUNCTION IF EXISTS fn_trg_stocks_update() CASCADE;
DROP FUNCTION IF EXISTS fn_trg_produk_name_change() CASCADE;

-- Trigger A: After a sale row is inserted into selling_details,
--   1) Decrement stock in Stocks
--   2) Log an 'OUT' entry in log_stok
CREATE OR REPLACE FUNCTION fn_trg_selling_detail()
RETURNS TRIGGER AS $$
BEGIN
    -- Set session flag so the Stocks trigger skips its own log
    -- (prevents double-logging: selling_details trigger already handles OUT)
    PERFORM set_config('app.skip_stock_log', 'true', true);

    -- Decrement stock
    UPDATE Stocks
    SET jumlah_stock = jumlah_stock - NEW.jumlah_keluar,
        timestamp = CURRENT_TIMESTAMP
    WHERE id_produk = NEW.id_produk;

    -- Log OUT activity
    INSERT INTO log_stok (tipe_aktivitas, id_user, nama_user, nama_produk, jumlah)
    SELECT 'OUT', s.id_kasir, u.username, p.nama_produk, NEW.jumlah_keluar
    FROM selling s
    JOIN Users u ON u.id_user = s.id_kasir
    JOIN Produk p ON p.id_produk = NEW.id_produk
    WHERE s.id_selling = NEW.id_selling;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Trigger B: After Stocks.jumlah_stock is updated,
--   Log an 'IN' entry when stock increases (e.g. initial stock, restock).
--   Skipped when the selling_details trigger already handled logging.
CREATE OR REPLACE FUNCTION fn_trg_stocks_update()
RETURNS TRIGGER AS $$
DECLARE
    v_skip BOOLEAN;
    v_increase INTEGER;
BEGIN
    -- Check session flag set by the selling_details trigger
    v_skip := COALESCE(current_setting('app.skip_stock_log', true)::boolean, false);
    IF v_skip THEN
        RETURN NEW;
    END IF;

    -- Only log when stock increases (initial fill or restock)
    IF OLD.jumlah_stock IS NOT NULL AND NEW.jumlah_stock > OLD.jumlah_stock THEN
        v_increase := NEW.jumlah_stock - OLD.jumlah_stock;

        INSERT INTO log_stok (tipe_aktivitas, id_user, nama_user, nama_produk, jumlah)
        SELECT 'IN', p.id_user, u.username, p.nama_produk, v_increase
        FROM Produk p
        JOIN Users u ON u.id_user = p.id_user
        WHERE p.id_produk = NEW.id_produk;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Trigger C: After Produk.nama_produk is changed,
--   Cascade the new name to all existing log_stok rows.
CREATE OR REPLACE FUNCTION fn_trg_produk_name_change()
RETURNS TRIGGER AS $$
BEGIN
    IF OLD.nama_produk IS DISTINCT FROM NEW.nama_produk THEN
        UPDATE log_stok
        SET nama_produk = NEW.nama_produk
        WHERE nama_produk = OLD.nama_produk;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Bind triggers to tables
CREATE TRIGGER trg_selling_detail_after_insert
    AFTER INSERT ON selling_details
    FOR EACH ROW EXECUTE FUNCTION fn_trg_selling_detail();

CREATE TRIGGER trg_stocks_after_update
    AFTER UPDATE OF jumlah_stock ON Stocks
    FOR EACH ROW EXECUTE FUNCTION fn_trg_stocks_update();

CREATE TRIGGER trg_produk_name_change
    AFTER UPDATE OF nama_produk ON Produk
    FOR EACH ROW EXECUTE FUNCTION fn_trg_produk_name_change();
