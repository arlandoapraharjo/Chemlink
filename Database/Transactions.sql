-- Transactions procedures for CHEMLINK (T-SQL)
-- Provides transactional procedures for Add (tambah), Delete (hapus), and Checkout features.
-- Uses explicit transactions and RAISERROR for errors.

CREATE OR ALTER PROCEDURE dbo.sp_add_order_detail_ts
	@id_order INT,
	@id_produk INT,
	@jumlah_masuk INT,
	@no_faktur VARCHAR(50) = NULL,
	@catatan VARCHAR(255) = NULL,
	@tanggal DATETIME = NULL
AS
BEGIN
	SET NOCOUNT ON;
	IF @tanggal IS NULL SET @tanggal = GETDATE();

	BEGIN TRY
		BEGIN TRANSACTION;

		-- Check stock
		IF EXISTS(SELECT 1 FROM Stocks s WHERE s.id_produk = @id_produk AND ISNULL(s.jumlah_stock,0) < @jumlah_masuk)
		BEGIN
			RAISERROR('Not enough stock for product.', 16, 1);
			ROLLBACK TRANSACTION;
			RETURN;
		END

		INSERT INTO order_details (tanggal, no_faktur, jumlah_masuk, catatan, id_order, id_produk)
		VALUES (@tanggal, @no_faktur, @jumlah_masuk, @catatan, @id_order, @id_produk);

		UPDATE Stocks
		SET jumlah_stock = ISNULL(jumlah_stock,0) - @jumlah_masuk,
			timestamp = GETDATE()
		WHERE id_produk = @id_produk;

		UPDATE orders
		SET jumlah = ISNULL(jumlah,0) + @jumlah_masuk
		WHERE id_order = @id_order;

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF XACT_STATE() <> 0
			ROLLBACK TRANSACTION;
		DECLARE @err NVARCHAR(4000) = ERROR_MESSAGE();
		RAISERROR(@err, 16, 1);
	END CATCH
END;


CREATE OR ALTER PROCEDURE dbo.sp_delete_order_ts
	@id_order INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRANSACTION;

		UPDATE s
		SET s.jumlah_stock = ISNULL(s.jumlah_stock,0) + d.jumlah_masuk,
			s.timestamp = GETDATE()
		FROM Stocks s
		JOIN order_details d ON s.id_produk = d.id_produk
		WHERE d.id_order = @id_order;

		DELETE FROM order_details WHERE id_order = @id_order;
		DELETE FROM orders WHERE id_order = @id_order;

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF XACT_STATE() <> 0
			ROLLBACK TRANSACTION;
		DECLARE @err NVARCHAR(4000) = ERROR_MESSAGE();
		RAISERROR(@err, 16, 1);
	END CATCH
END;


-- For checkout, pass a table-valued parameter. Create the type if not exists.
IF TYPE_ID(N'dbo.OrderDetailType') IS NULL
BEGIN
	CREATE TYPE dbo.OrderDetailType AS TABLE
	(
		id_produk INT NOT NULL,
		jumlah_masuk INT NOT NULL,
		no_faktur VARCHAR(50) NULL,
		catatan VARCHAR(255) NULL,
		harga INT NULL
	);
END

CREATE OR ALTER PROCEDURE dbo.sp_checkout_order_ts
	@tanggal DATETIME,
	@keterangan_order NVARCHAR(255),
	@input_by INT,
	@details dbo.OrderDetailType READONLY,
	@new_order_id INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRANSACTION;

		IF EXISTS(
			SELECT 1 FROM @details d JOIN Stocks s ON s.id_produk = d.id_produk
			WHERE ISNULL(s.jumlah_stock,0) < d.jumlah_masuk
		)
		BEGIN
			RAISERROR('Insufficient stock for one or more items.', 16, 1);
			ROLLBACK TRANSACTION;
			RETURN;
		END

		DECLARE @total_items INT = (SELECT ISNULL(SUM(jumlah_masuk),0) FROM @details);

		INSERT INTO orders (tanggal, jumlah, keterangan_order, input_by)
		VALUES (@tanggal, @total_items, @keterangan_order, @input_by);
		SET @new_order_id = SCOPE_IDENTITY();

		INSERT INTO order_details (tanggal, no_faktur, jumlah_masuk, catatan, id_order, id_produk)
		SELECT ISNULL(d.no_faktur, CONVERT(VARCHAR(19), @tanggal, 120)), d.no_faktur, d.jumlah_masuk, d.catatan, @new_order_id, d.id_produk
		FROM @details d;

		UPDATE s
		SET s.jumlah_stock = ISNULL(s.jumlah_stock,0) - d.total_qty,
			s.timestamp = GETDATE()
		FROM Stocks s
		JOIN (
			SELECT id_produk, SUM(jumlah_masuk) AS total_qty FROM @details GROUP BY id_produk
		) d ON s.id_produk = d.id_produk;

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF XACT_STATE() <> 0
			ROLLBACK TRANSACTION;
		DECLARE @err NVARCHAR(4000) = ERROR_MESSAGE();
		RAISERROR(@err, 16, 1);
	END CATCH
END;
