-- Create schema
CREATE SCHEMA IF NOT EXISTS chemlink_sch;
SET search_path TO chemlink_sch, public;

-- Drop triggers (stock/log handled explicitly in C# transaction)
DROP TRIGGER IF EXISTS trg_stok_keluar ON selling_details;
DROP TRIGGER IF EXISTS trg_log_stok_keluar ON selling_details;
DROP FUNCTION IF EXISTS fn_trg_stok_keluar() CASCADE;
DROP FUNCTION IF EXISTS fn_trg_log_stok_keluar() CASCADE;
DROP TABLE IF EXISTS log_stok CASCADE;
DROP TABLE IF EXISTS selling_details CASCADE;
DROP TABLE IF EXISTS selling CASCADE;
DROP TABLE IF EXISTS Stocks CASCADE;
DROP TABLE IF EXISTS Produk CASCADE;
DROP TABLE IF EXISTS Supplier CASCADE;
DROP TABLE IF EXISTS Users CASCADE;
DROP TABLE IF EXISTS Kategori CASCADE;

-- ============================================================
-- TABLES
-- ============================================================

CREATE TABLE Kategori (
    id_kategori SERIAL PRIMARY KEY,
    nama_kategori VARCHAR(100) NOT NULL
);

CREATE TABLE Users (
    id_user SERIAL PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    Role VARCHAR(50),
    fullname VARCHAR(150),
    status VARCHAR(50),
    alamat TEXT,
    no_telp VARCHAR(20) UNIQUE,
    email VARCHAR(100) UNIQUE,
    kota VARCHAR(100),
    kecamatan VARCHAR(100)
);

CREATE TABLE Supplier (
    id_supplier SERIAL PRIMARY KEY,
    nama_perusahaan VARCHAR(150) NOT NULL,
    kontak_person VARCHAR(100),
    no_telp VARCHAR(20) UNIQUE,
    email VARCHAR(100) UNIQUE,
    alamat_supplier TEXT,
    kota_supplier VARCHAR(100),
    status VARCHAR(50)
);

CREATE TABLE Produk (
    id_produk SERIAL PRIMARY KEY,
    nama_produk VARCHAR(150) NOT NULL,
    harga INTEGER,
    keterangan TEXT,
    id_kategori INTEGER REFERENCES Kategori(id_kategori),
    id_supplier INTEGER REFERENCES Supplier(id_supplier),
    id_user INTEGER REFERENCES Users(id_user)
);

CREATE TABLE Stocks (
    id_stock SERIAL PRIMARY KEY,
    jumlah_stock INTEGER,
    timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    id_produk INTEGER REFERENCES Produk(id_produk)
);

CREATE TABLE selling (
    id_selling SERIAL PRIMARY KEY,
    tanggal_selling DATE,
    keterangan TEXT,
    id_kasir INTEGER REFERENCES Users(id_user)
);

CREATE TABLE selling_details (
    id_detail SERIAL PRIMARY KEY,
    jumlah_keluar INTEGER,
    id_selling INTEGER REFERENCES selling(id_selling),
    id_produk INTEGER REFERENCES Produk(id_produk)
);

CREATE TABLE log_stok (
    id_log SERIAL PRIMARY KEY,
    tipe_aktivitas VARCHAR(10),
    id_user INTEGER,
    nama_user VARCHAR(255),
    nama_produk VARCHAR(150),
    jumlah INTEGER,
    time_stamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Trigger functions removed: stock decrement and log are now
-- handled explicitly in C# OrderContext.Checkout() transaction.

-- ============================================================
-- SEED DATA
-- ============================================================

INSERT INTO Kategori (nama_kategori) VALUES
('Herbisida'),
('Fungisida'),
('Insektisida'),
('Pupuk');

INSERT INTO Users (username, password, Role, fullname, status, alamat, no_telp, email, kota, kecamatan) VALUES
('admin', 'admin123', 'Admin', 'Administrator', 'Active', 'Jl. Merdeka No.1', '08111111111', 'admin@chemlink.com', 'Surabaya', 'Gubeng'),
('kasir', 'kasir123', 'Kasir', 'Kasir Utama', 'Active', 'Jl. Pemuda No.5', '08222222222', 'kasir@chemlink.com', 'Surabaya', 'Tegalsari');

INSERT INTO Supplier (nama_perusahaan, kontak_person, no_telp, email, alamat_supplier, kota_supplier, status) VALUES
('PT Agro Sentosa', 'Budi Santoso', '08123456789', 'info@agrosentosa.com', 'Jl. Raya Industri No.10', 'Surabaya', 'Active'),
('CV Tani Makmur', 'Siti Rahayu', '08987654321', 'info@tanimakmur.com', 'Jl. Tani Raya No.25', 'Malang', 'Active');

INSERT INTO Produk (nama_produk, harga, keterangan, id_kategori, id_supplier, id_user) VALUES
('Gramoxone 276SL 1L', 85000, 'Herbisida kontak untuk gulma', 1, 1, 1),
('Antracol 70WP 500gr', 95000, 'Fungisida pencegah jamur', 2, 1, 1),
('Furadan 3GR 2kg', 45000, 'Insektisida butiran untuk hama tanah', 3, 2, 1),
('Roundup 486SL 1L', 105000, 'Herbisida sistemik purna tumbuh', 1, 2, 1);

-- Stock values reflect post-sale amounts (seed sales below already deducted)
INSERT INTO Stocks (jumlah_stock, id_produk) VALUES
(22, 1),
(3, 2),
(35, 3),
(2, 4);

INSERT INTO selling (tanggal_selling, keterangan, id_kasir) VALUES
('2025-06-05', 'Penjualan ke petani lokal', 2),
('2025-06-12', 'Penjualan toko tani', 2);

-- Seed selling details
INSERT INTO selling_details (jumlah_keluar, id_selling, id_produk) VALUES
(3, 1, 1),
(2, 1, 2),
(5, 2, 3);

-- Stock activity log (IN = initial stock, OUT = seed sales)
INSERT INTO log_stok (tipe_aktivitas, id_user, nama_user, nama_produk, jumlah) VALUES
('IN', 1, 'admin', 'Gramoxone 276SL 1L', 25),
('IN', 1, 'admin', 'Antracol 70WP 500gr', 5),
('IN', 1, 'admin', 'Furadan 3GR 2kg', 40),
('IN', 1, 'admin', 'Roundup 486SL 1L', 2),
('OUT', 2, 'kasir', 'Gramoxone 276SL 1L', 3),
('OUT', 2, 'kasir', 'Antracol 70WP 500gr', 2),
('OUT', 2, 'kasir', 'Furadan 3GR 2kg', 5);
