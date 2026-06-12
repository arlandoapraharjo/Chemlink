# Rekomendasi Penggunaan Objek Database (Chemlink)

Berikut adalah alternatif penerapan Stored Procedure, Function, View, dan Transaction yang cocok untuk digunakan pada sistem Chemlink berdasarkan struktur tabel yang ada.

## 1. Views
Digunakan untuk menyederhanakan query `SELECT` yang kompleks, terutama yang melibatkan banyak `JOIN`. View sangat cocok untuk fitur menampilkan data (seperti di halaman dashboard atau laporan).

*   **`v_detail_produk`**: Menampilkan informasi lengkap produk beserta nama kategori, nama supplier, dan jumlah stok saat ini.
    *   *Tabel yang terlibat*: `Produk`, `Kategori`, `Supplier`, `Stocks`
*   **`v_ringkasan_pesanan`**: Menampilkan daftar pesanan (`orders`) beserta nama user yang menginput pesanan tersebut.
    *   *Tabel yang terlibat*: `orders`, `"User"`
*   **`v_stok_kritis`**: Menampilkan produk yang jumlah stoknya di bawah batas minimum (misalnya < 10). Sangat berguna untuk notifikasi sistem.
    *   *Tabel yang terlibat*: `Produk`, `Stocks`

## 2. Functions
Digunakan untuk melakukan perhitungan spesifik dan mengembalikan nilai, yang dapat disisipkan ke dalam query `SELECT` lainnya.

*   **`fn_hitung_total_pesanan(p_id_order INT)`**: Menghitung total jumlah barang dari sebuah pesanan berdasarkan data di `order_details`.
*   **`fn_cek_ketersediaan_stok(p_id_produk INT, p_jumlah_diminta INT)`**: Mengembalikan nilai boolean (`TRUE`/`FALSE`) yang mengecek apakah stok suatu produk mencukupi untuk jumlah yang direquest.
*   **`fn_get_harga_produk(p_id_produk INT)`**: Mengambil harga terkini dari produk untuk keperluan perhitungan cepat tanpa perlu melakukan JOIN tabel.

## 3. Stored Procedures
Digunakan untuk mengeksekusi serangkaian pernyataan SQL, biasanya untuk operasi `INSERT`, `UPDATE`, atau `DELETE` yang melibatkan logika bisnis untuk memusatkan proses di sisi database.

*   **`sp_tambah_produk_baru`**: Menambahkan data produk baru ke tabel `Produk` dan secara otomatis menginisialisasi entri stok awal (misal: 0) di tabel `Stocks`.
*   **`sp_catat_pesanan_masuk`**: Digunakan untuk menginput pesanan baru yang secara internal dapat memanggil berbagai query INSERT.
*   **`sp_update_status_user`**: Mengubah status aktif/non-aktif user (misalnya menjadi 'Inactive').

## 4. Transactions
Digunakan untuk memastikan integritas data ketika menjalankan operasi multi-step. Transaction memastikan prinsip ACID (jika satu langkah gagal, semua perubahan di-rollback).

*   **Transaksi Pembuatan Pesanan / Order Baru**: 
    1. `BEGIN TRANSACTION`
    2. `INSERT` ke tabel `orders`.
    3. Mendapatkan ID `id_order` yang baru saja dibuat.
    4. Melakukan beberapa `INSERT` ke tabel `order_details` untuk setiap barang.
    5. `COMMIT`. *Jika terjadi error di tengah proses, lakukan `ROLLBACK` agar tidak ada order "yatim" (tanpa detail).*
*   **Transaksi Update Stok Barang Masuk / Keluar**:
    1. `BEGIN TRANSACTION`
    2. `INSERT` data riwayat/mutasi ke `order_details`.
    3. `UPDATE` jumlah pada tabel `Stocks` (ditambah/dikurang sesuai tipe transaksi).
    4. `COMMIT`. *Mencegah inkonsistensi dimana barang tercatat masuk tapi jumlah stok tidak bertambah.*
