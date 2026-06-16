CREATE TABLE Kategori (
    id_kategori SERIAL PRIMARY KEY,
    nama_kategori VARCHAR(100) NOT NULL
);

CREATE TABLE Users (
    id_user SERIAL PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    Role VARCHAR(50),
	fullname VARCHAR(255),
    status VARCHAR(50),
    alamat TEXT,
    no_telp VARCHAR(20) UNIQUE,
    email VARCHAR(100) UNIQUE,
    kecamatan VARCHAR(100)
);

CREATE TABLE Supplier (
    id_supplier SERIAL PRIMARY KEY,
    nama_perusahaan VARCHAR(150) NOT NULL,
    status VARCHAR(50),
    kontak_person VARCHAR(100),
    no_telp VARCHAR(20) UNIQUE,
    email VARCHAR(100) UNIQUE,
    alamat_supplier TEXT,
    kota_supplier VARCHAR(100)
);

CREATE TABLE Produk (
    id_produk SERIAL PRIMARY KEY,
    nama_produk VARCHAR(150) NOT NULL,
    harga INTEGER,
    tanggal_exp DATE,
    keterangan TEXT,
    id_kategori INTEGER REFERENCES Kategori(id_kategori),
    id_supplier INTEGER REFERENCES Supplier(id_supplier)
);

CREATE TABLE Stocks (
    id_stock SERIAL PRIMARY KEY,
    jumlah_stock INTEGER,
    time_stamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    id_produk INTEGER REFERENCES Produk(id_produk)
);

CREATE TABLE orders (
    id_order SERIAL PRIMARY KEY,
	no_faktur VARCHAR(100),
    tanggal_order DATE,
    keterangan_order TEXT,
    input_by INTEGER REFERENCES Users(id_user)
);

CREATE TABLE order_details (
    id_tm SERIAL PRIMARY KEY,
    jumlah_masuk INTEGER,
    catatan TEXT,
    id_order INTEGER REFERENCES orders(id_order),
    id_produk INTEGER REFERENCES Produk(id_produk)
);

CREATE TABLE selling (
    id_selling SERIAL PRIMARY KEY,
    no_faktur VARCHAR(100),
    tanggal_selling DATE,
    keterangan TEXT,
    id_kasir INTEGER REFERENCES Users(id_user)
);

CREATE TABLE selling_details (
    id_detail SERIAL PRIMARY KEY,
    jumlah_keluar INTEGER,
    catatan TEXT,
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

INSERT INTO Kategori (nama_kategori) VALUES 
('Herbisida'),
('Fungisida'),
('Insektisida'),
('Pupuk');

INSERT INTO Users (username, password, Role, fullname, alamat, kecamatan, no_telp, email, status) VALUES 
('admin', 'admin123', 'Admin', 'Nama Lengkap Admin', 'Jl. Alamat Admin', 'Jenggawah', '0812345677899', 'email admin', 'Active'),
('kasir', 'kasir123', 'Kasir', 'Nama Lengkap Kasir', 'Jl. Alamat Kasir', 'Kaliwates', '0812345677898', 'email kasir', 'Active');

INSERT INTO Supplier (nama_perusahaan, kontak_person, no_telp, alamat_supplier, kota_supplier, email, status) VALUES 
('PT Agro Sentosa', 'Andi', '08123456789', 'Jl. Supplier Agro', 'Surabaya', 'email supplier agro', 'Active'),
('CV Tani Makmur', 'Salim', '08987654321', 'Jl. Supplier Tani', 'Malang', 'email supplier tani', 'Active');

INSERT INTO Produk (nama_produk, harga, tanggal_exp, keterangan, id_kategori, id_supplier) VALUES
('Gramoxone 276SL 1L', 85000, '2026-09-06', 'Supaya tumbuh subur seperti di Grow a Garden', 1, 1),
('Antracol 70WP 500gr', 95000, '2026-09-06', 'Untuk menghilangkan jamur pada Tembakau', 2, 1),
('Furadan 3GR 2kg', 45000, '2026-09-06', 'Untuk mengusir serangga yang mendekat', 3, 2),
('Roundup 486SL 1L', 105000, '2026-09-06', 'Supaya tumbuh besar seperti di Grow a Garden', 4, 2);

INSERT INTO Stocks (jumlah_stock, id_produk) VALUES 
(25, 1),
(5, 2),
(40, 3),
(2, 4);