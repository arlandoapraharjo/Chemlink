# Sequence Diagram — POS Checkout Flow

```mermaid
sequenceDiagram
    actor Kasir
    participant POSControl as POSControl (View)
    participant UserController as UserController
    participant OrderContext as OrderContext
    participant PostgreSQL as PostgreSQL DB

    Note over Kasir, PostgreSQL: === Tambah ke Keranjang ===

    Kasir->>POSControl: Pilih produk + input qty
    POSControl->>POSControl: BtnAddCart_Click()
    POSControl->>UserController: AddCartEvent(SelectedProduct, Qty)
    UserController->>UserController: Validasi: qty > 0 & stok cukup
    UserController->>UserController: _cart.Add(CartItem)
    UserController->>UserController: GetDisplayProducts(_products)
    UserController->>POSControl: ShowPOS(display, _cart)
    Note right of POSControl: Stok ditampilkan = DB stok - qty keranjang

    Note over Kasir, PostgreSQL: === Hapus dari Keranjang ===

    Kasir->>POSControl: Pilih item di keranjang + klik Hapus
    POSControl->>UserController: DeleteCartEvent(CartItem)
    UserController->>UserController: _cart.Remove(item)
    UserController->>UserController: GetDisplayProducts(_products)
    UserController->>POSControl: ShowPOS(display, _cart)
    Note right of POSControl: Stok otomatis kembali (tidak ada mutasi)

    Note over Kasir, PostgreSQL: === Checkout / Bayar ===

    Kasir->>POSControl: Klik tombol Checkout
    POSControl->>UserController: CheckoutEvent()
    UserController->>UserController: Validasi: _cart tidak kosong
    
    rect rgb(230, 245, 255)
        Note over UserController, PostgreSQL: Transaksi Database (Atomic)
        UserController->>OrderContext: Checkout(_cart, userId, "Penjualan Kasir")
        OrderContext->>PostgreSQL: BEGIN TRANSACTION
        
        OrderContext->>PostgreSQL: INSERT INTO selling (tanggal, keterangan, id_kasir)
        PostgreSQL-->>OrderContext: RETURNING id_selling
        
        loop Untuk setiap item di keranjang
            OrderContext->>PostgreSQL: INSERT INTO selling_details (jumlah_keluar, id_selling, id_produk)
            OrderContext->>PostgreSQL: UPDATE Stocks SET jumlah_stock = jumlah_stock - qty
            OrderContext->>PostgreSQL: INSERT INTO log_stok ('OUT', user, produk, qty)
        end
        
        OrderContext->>PostgreSQL: COMMIT
        PostgreSQL-->>OrderContext: OK
        OrderContext-->>UserController: Selesai
    end

    UserController->>UserController: Hitung total belanja
    UserController->>UserController: Generate struk belanja
    UserController->>POSControl: PrintReceipt(struk)
    
    UserController->>UserController: _cart.Clear()
    UserController->>UserController: _products = Read() dari DB
    UserController->>POSControl: ShowPOS(display, empty cart)
    Note right of POSControl: Stok sudah berkurang (dari DB)

    Note over Kasir, PostgreSQL: === Jika Terjadi Error ===
    
    alt Exception terjadi saat checkout
        OrderContext->>PostgreSQL: ROLLBACK
        PostgreSQL-->>OrderContext: OK
        OrderContext-->>UserController: throw Exception
        UserController->>POSControl: ShowMessage("Gagal: " + error)
    end
```

## Penjelasan Alur

### 1. Tambah ke Keranjang
- Kasir memilih produk dari grid POS dan memasukkan jumlah
- `POSControl` mengirim event ke `UserController`
- Item ditambahkan ke list `_cart` (in-memory)
- Tampilan stok dihitung ulang: `Stok DB - Qty Keranjang`
- **Tidak ada perubahan ke database**

### 2. Hapus dari Keranjang
- Kasir memilih item di grid keranjang dan klik Hapus
- Item dihapus dari list `_cart`
- Tampilan stok dihitung ulang otomatis
- **Tidak ada mutasi objek produk**

### 3. Checkout (Persist ke Database)
- Semua operasi dilakukan dalam **satu transaksi database atomik**
- Untuk setiap item keranjang:
  1. `INSERT` ke tabel `selling_details`
  2. `UPDATE` tabel `Stocks` (kurangi stok)
  3. `INSERT` ke tabel `log_stok` (catat aktivitas OUT)
- Jika semua berhasil → `COMMIT`
- Jika ada error → `ROLLBACK` (tidak ada perubahan)
