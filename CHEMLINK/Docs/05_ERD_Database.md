# Entity Relationship Diagram (ERD) — ChemLink Database

Schema: `chemlink_sch`

```mermaid
erDiagram
    Kategori {
        SERIAL id_kategori PK
        VARCHAR_100 nama_kategori NOT_NULL
    }

    Users {
        SERIAL id_user PK
        VARCHAR_50 username UNIQUE_NOT_NULL
        VARCHAR_255 password NOT_NULL
        VARCHAR_50 Role
        VARCHAR_150 fullname
        VARCHAR_50 status
        TEXT alamat
        VARCHAR_20 no_telp UNIQUE
        VARCHAR_100 email UNIQUE
        VARCHAR_100 kota
        VARCHAR_100 kecamatan
    }

    Supplier {
        SERIAL id_supplier PK
        VARCHAR_150 nama_perusahaan NOT_NULL
        VARCHAR_100 kontak_person
        VARCHAR_20 no_telp UNIQUE
        VARCHAR_100 email UNIQUE
        TEXT alamat_supplier
        VARCHAR_100 kota_supplier
        VARCHAR_50 status
    }

    Produk {
        SERIAL id_produk PK
        VARCHAR_150 nama_produk NOT_NULL
        INTEGER harga
        TEXT keterangan
        INTEGER id_kategori FK
        INTEGER id_supplier FK
        INTEGER id_user FK
    }

    Stocks {
        SERIAL id_stock PK
        INTEGER jumlah_stock
        TIMESTAMP timestamp DEFAULT_NOW
        INTEGER id_produk FK
    }

    selling {
        SERIAL id_selling PK
        DATE tanggal_selling
        TEXT keterangan
        INTEGER id_kasir FK
    }

    selling_details {
        SERIAL id_detail PK
        INTEGER jumlah_keluar
        INTEGER id_selling FK
        INTEGER id_produk FK
    }

    log_stok {
        SERIAL id_log PK
        VARCHAR_10 tipe_aktivitas
        INTEGER id_user
        VARCHAR_255 nama_user
        VARCHAR_150 nama_produk
        INTEGER jumlah
        TIMESTAMP time_stamp DEFAULT_NOW
    }

    Kategori ||--o{ Produk : "1:N memiliki"
    Supplier ||--o{ Produk : "1:N menyuplai"
    Users ||--o{ Produk : "1:N menambahkan"
    Produk ||--o| Stocks : "1:1 memiliki stok"
    Produk ||--o{ selling_details : "1:N dijual di"
    Users ||--o{ selling : "1:N sebagai kasir"
    selling ||--o{ selling_details : "1:N memiliki detail"
```

## Database Views (chemlink_sch)

| View | Deskripsi | Sumber Data |
|------|-----------|-------------|
| `v_detail_produk` | Detail produk + kategori + supplier + stok | Produk, Kategori, Supplier, Stocks |
| `v_stok_kritis` | Produk dengan stok ≤ 5 | v_detail_produk |
| `v_laporan_keuangan` | Laporan pendapatan dari penjualan | selling, selling_details, Produk |
| `v_log_stok` | Log aktivitas stok (IN/OUT) | log_stok |
| `v_show_supplier` | Daftar supplier aktif | Supplier |

## Stored Procedures (chemlink_sch)

| Procedure | Fungsi |
|-----------|--------|
| `sp_tambah_produk_baru` | Insert produk baru + create Stocks row (stok=0) |
| `sp_tambah_user` | Insert user baru dengan validasi |
| `sp_update_status_user` | Soft delete user (ubah status ke Inactive) |
| `sp_tambah_supplier` | Insert supplier baru |
| `sp_transaksi_selling` | Legacy — checkout via stored procedure (tidak digunakan) |

## Relationships

| Relasi | Tipe | Penjelasan |
|--------|------|------------|
| Kategori → Produk | 1:N | Satu kategori memiliki banyak produk |
| Supplier → Produk | 1:N | Satu supplier menyuplai banyak produk |
| Users → Produk | 1:N | Satu user menambahkan banyak produk |
| Produk → Stocks | 1:1 | Satu produk memiliki satu record stok |
| Produk → selling_details | 1:N | Satu produk bisa dijual di banyak transaksi |
| Users → selling | 1:N | Satu kasir melakukan banyak penjualan |
| selling → selling_details | 1:N | Satu penjualan memiliki banyak detail item |
