
CREATE TABLE Kategori (
    id_kategori SERIAL PRIMARY KEY,
    nama_kategori VARCHAR(100) NOT NULL
);

CREATE TABLE "User" (
    id_user SERIAL PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    Role VARCHAR(50),
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
    merek VARCHAR(100),
    satuan VARCHAR(50),
    harga INTEGER,
    tanggal_exp DATE,
    keterangan TEXT,
    id_kategori INTEGER REFERENCES Kategori(id_kategori),
    id_supplier INTEGER REFERENCES Supplier(id_supplier),
    id_user INTEGER REFERENCES "User"(id_user)
);

CREATE TABLE Stocks (
    id_stock SERIAL PRIMARY KEY,
    jumlah_stock INTEGER,
    timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    id_produk INTEGER REFERENCES Produk(id_produk)
);

CREATE TABLE orders (
    id_order SERIAL PRIMARY KEY,
    tanggal DATE,
    jumlah INTEGER,
    keterangan_order TEXT,
    input_by INTEGER REFERENCES "User"(id_user)
);

CREATE TABLE order_details (
    id_tm SERIAL PRIMARY KEY,
    tanggal DATE,
    no_faktur VARCHAR(100),
    jumlah_masuk INTEGER,
    catatan TEXT,
    id_order INTEGER REFERENCES orders(id_order),
    id_produk INTEGER REFERENCES Produk(id_produk)
);

-- DUMMY DATA INSERTION

INSERT INTO Kategori (nama_kategori) VALUES 
('Herbisida'),
('Fungisida'),
('Insektisida'),
('Pupuk');

INSERT INTO "User" (username, password, Role, status) VALUES 
('admin', 'admin123', 'Admin', 'Active'),
('kasir', 'kasir123', 'Kasir', 'Active');

INSERT INTO Supplier (nama_perusahaan, no_telp, kota_supplier, status) VALUES 
('PT Agro Sentosa', '08123456789', 'Surabaya', 'Active'),
('CV Tani Makmur', '08987654321', 'Malang', 'Active');

INSERT INTO Produk (nama_produk, harga, id_kategori, id_supplier, id_user) VALUES 
('Gramoxone 276SL 1L', 85000, 1, 1, 1),
('Antracol 70WP 500gr', 95000, 2, 1, 1),
('Furadan 3GR 2kg', 45000, 3, 2, 1),
('Roundup 486SL 1L', 105000, 1, 2, 1);

INSERT INTO Stocks (jumlah_stock, id_produk) VALUES 
(25, 1),
(5, 2),
(40, 3),
(2, 4);