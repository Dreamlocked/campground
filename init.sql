IF EXISTS (SELECT name FROM sys.databases WHERE name = 'campground')
DROP DATABASE campground
GO

CREATE DATABASE campground
GO
USE campground
GO

/*============================================================================*/
/*                                  TABLES                                    */
/*============================================================================*/
CREATE TABLE [campgrounds] (
  [id]                  UNIQUEIDENTIFIER NOT NULL,
  [host_id]             UNIQUEIDENTIFIER NOT NULL,
  [title]               VARCHAR(200),
  [latitude]            DECIMAL(15,9),
  [longitude]           DECIMAL(15,9),
  [price_per_night]     DECIMAL(15,9) NOT NULL,
  [description]         VARCHAR(MAX),
  [location]            VARCHAR(200),
  [create_at]           DATETIME,
  [enable]              BIT,
CONSTRAINT [PK_3] PRIMARY KEY ([id])
)
GO

CREATE TABLE [users] (
  [id]             UNIQUEIDENTIFIER NOT NULL,
  [username]       VARCHAR(200),
  [first_name]     VARCHAR(200),
  [last_name]      VARCHAR(200),
  [salt]           VARCHAR(MAX),
  [password]       VARCHAR(MAX),
  [email]          VARCHAR(200),
  [url_photo]      VARCHAR(MAX),
CONSTRAINT [PK_6] PRIMARY KEY ([id])
)
GO

CREATE TABLE [bookings] (
  [id]                   UNIQUEIDENTIFIER NOT NULL,
  [campground_id]        UNIQUEIDENTIFIER NOT NULL,
  [user_id]              UNIQUEIDENTIFIER NOT NULL,
  [arriving_date]        DATETIME NOT NULL,
  [leaving_date]         DATETIME NOT NULL,
  [create_at]            DATETIME,
  [paid]                 BIT DEFAULT 0,
  [attended]             BIT DEFAULT 0
CONSTRAINT [PK_9] PRIMARY KEY ([id])
)
GO

CREATE TABLE [images] (
  [id]                 UNIQUEIDENTIFIER NOT NULL,
  [campgrounds_id]     UNIQUEIDENTIFIER NOT NULL,
  [filename]           VARCHAR(200),
  [url]                VARCHAR(MAX),
  [alt]                VARCHAR(200),
CONSTRAINT [PK_10] PRIMARY KEY ([id])
)
GO

CREATE TABLE [notifications] (
  [id]            UNIQUEIDENTIFIER NOT NULL,
  [user_id]       UNIQUEIDENTIFIER NOT NULL,
  [message]       VARCHAR(MAX) NOT NULL,
  [create_at]     DATETIME NOT NULL,
  [viewed]        BIT DEFAULT 0,
CONSTRAINT [PK_11] PRIMARY KEY ([id])
)
GO

CREATE TABLE [reviews] (
  [id]             UNIQUEIDENTIFIER NOT NULL,
  [booking_id]     UNIQUEIDENTIFIER NOT NULL UNIQUE,
  [comment]        VARCHAR(MAX),
  [rating]         INT,
  [create_at]      DATETIME,
CONSTRAINT [PK_14] PRIMARY KEY ([id])
)
GO

/*============================================================================*/
/*                               FOREIGN KEYS                                 */
/*============================================================================*/
ALTER TABLE [campgrounds]
    ADD CONSTRAINT [FK_user_campgrounds]
        FOREIGN KEY ([host_id])
            REFERENCES [users] ([id])
ON DELETE CASCADE
 GO

ALTER TABLE [bookings]
    ADD CONSTRAINT [FK_user_bookings]
        FOREIGN KEY ([user_id])
            REFERENCES [users] ([id])
ON DELETE CASCADE
 GO

ALTER TABLE [bookings]
    ADD CONSTRAINT [FK_campground_bookings]
        FOREIGN KEY ([campground_id])
            REFERENCES [campgrounds] ([id])
 GO

ALTER TABLE [images]
    ADD CONSTRAINT [FK_campground_images]
        FOREIGN KEY ([campgrounds_id])
            REFERENCES [campgrounds] ([id])
ON DELETE CASCADE
 GO

ALTER TABLE [reviews]
    ADD CONSTRAINT [FK_REFERENCE_1]
        FOREIGN KEY ([booking_id])
            REFERENCES [bookings] ([id])
 GO
