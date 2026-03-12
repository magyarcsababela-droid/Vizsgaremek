-- 1. Users & Addresses -----------------------------------------------------
CREATE TABLE Users (
    id                BIGINT IDENTITY(1,1) PRIMARY KEY,
    username          VARCHAR(50) NOT NULL UNIQUE,
    email             VARCHAR(255) NOT NULL UNIQUE,
    password_hash     CHAR(60) NOT NULL,        -- bcrypt hash
    created_at        DATETIMEOFFSET DEFAULT SYSUTCDATETIME(),
	role			  VARCHAR(50)
);

CREATE TABLE Addresses (
    id                BIGINT IDENTITY(1,1) PRIMARY KEY,
    user_id           BIGINT NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    street            VARCHAR(255) NOT NULL,
    city              VARCHAR(100) NOT NULL,
    state             VARCHAR(100),
    zip               VARCHAR(20),
    country           VARCHAR(100) NOT NULL,
    is_default        BIT DEFAULT 0
);

-- 2. Categories & Products -------------------------------------------------
CREATE TABLE Categories (
    id                BIGINT IDENTITY(1,1) PRIMARY KEY,
    name              VARCHAR(50) NOT NULL UNIQUE,
    description       NVARCHAR(MAX)
);

CREATE TABLE Products (
    id                BIGINT IDENTITY(1,1) PRIMARY KEY,
    category_id       BIGINT NOT NULL REFERENCES categories(id),
    sku               VARCHAR(100) NOT NULL UNIQUE,
    name              VARCHAR(200) NOT NULL,
    description       NVARCHAR(MAX),
    base_price        NUMERIC(10,2) NOT NULL CHECK (base_price >= 0),
    image_url         NVARCHAR(MAX)
);

CREATE TABLE Inventory_products (
    product_id        BIGINT PRIMARY KEY REFERENCES products(id) ON DELETE CASCADE,
    quantity_available INT NOT NULL DEFAULT 0 CHECK (quantity_available >= 0)
);

-- 3. Component Types & Components ------------------------------------------
CREATE TABLE Component_type (
    id                BIGINT IDENTITY(1,1) PRIMARY KEY,
    name              VARCHAR(50) NOT NULL UNIQUE,
    description       NVARCHAR(MAX)
);

CREATE TABLE Components (
    id                BIGINT IDENTITY(1,1) PRIMARY KEY,
    type_id           BIGINT NOT NULL REFERENCES component_type(id),
    sku               VARCHAR(100) NOT NULL UNIQUE,
    name              VARCHAR(200) NOT NULL,
    description       NVARCHAR(MAX),
    price             NUMERIC(10,2) NOT NULL CHECK (price >= 0)
);

CREATE TABLE Inventory_components (
	id                BIGINT IDENTITY(1,1) PRIMARY KEY,
    component_id      BIGINT PRIMARY KEY REFERENCES components(id) ON DELETE CASCADE,
    quantity_available INT NOT NULL DEFAULT 0 CHECK (quantity_available >= 0)
);

-- 4. Pre-built PCs ----------------------------------------------------------
CREATE TABLE Prebuilt_pcs (
    pc_id             BIGINT IDENTITY(1,1) PRIMARY KEY,
    product_id        BIGINT NOT NULL UNIQUE REFERENCES products(id) ON DELETE CASCADE,
    assembly_fee      NUMERIC(10,2) DEFAULT 0 CHECK (assembly_fee >= 0)
);

CREATE TABLE Prebuilt_pc_comp (
	id                BIGINT IDENTITY(1,1) PRIMARY KEY,
    pc_id             BIGINT NOT NULL REFERENCES prebuilt_pcs(pc_id) ON DELETE CASCADE,
    component_id      BIGINT NOT NULL REFERENCES components(id) ON DELETE CASCADE,
    quantity          INT NOT NULL DEFAULT 1 CHECK (quantity > 0),
    --PRIMARY KEY (pc_id, component_id)
);

-- 5. Custom Builds ----------------------------------------------------------
CREATE TABLE Custom_builds (
    build_id          BIGINT IDENTITY(1,1) PRIMARY KEY,
    user_id           BIGINT NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    name              VARCHAR(200) NOT NULL,
    status            VARCHAR(20) NOT NULL CHECK (status IN ('draft','ordered','completed')),
    total_price       NUMERIC(12,2) NOT NULL DEFAULT 0 CHECK (total_price >= 0),
    created_at        DATETIMEOFFSET DEFAULT SYSUTCDATETIME()
);

CREATE TABLE Build_components (
	id				  BIGINT IDENTITY(1,1) PRIMARY KEY,
    build_id          BIGINT NOT NULL REFERENCES custom_builds(build_id) ON DELETE CASCADE,
    component_id      BIGINT NOT NULL REFERENCES components(id) ON DELETE CASCADE,
    quantity          INT NOT NULL DEFAULT 1 CHECK (quantity > 0),
    unit_price        NUMERIC(10,2) NOT NULL CHECK (unit_price >= 0),
    --PRIMARY KEY (build_id, component_id)
);

-- 6. Orders & Order Items --------------------------------------------------
CREATE TABLE Orders (
    order_id          BIGINT IDENTITY(1,1) PRIMARY KEY,
    user_id           BIGINT NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    shipping_address_id BIGINT NOT NULL REFERENCES addresses(id),
    status            VARCHAR(20) NOT NULL CHECK (status IN ('pending','processing','shipped','delivered','canceled')),
    payment_method    VARCHAR(50) NOT NULL,
    total_amount      NUMERIC(12,2) NOT NULL DEFAULT 0 CHECK (total_amount >= 0),
    placed_at         DATETIMEOFFSET DEFAULT SYSUTCDATETIME()
);

-- Pre-built PC / other product items
CREATE TABLE Order_items_p (
    item_id           BIGINT IDENTITY(1,1) PRIMARY KEY,
    order_id          BIGINT NOT NULL REFERENCES orders(order_id) ON DELETE CASCADE,
    product_id        BIGINT NOT NULL REFERENCES products(id),
    quantity          INT NOT NULL DEFAULT 1 CHECK (quantity > 0),
    unit_price        NUMERIC(10,2) NOT NULL CHECK (unit_price >= 0),
    total_price       AS (quantity * unit_price) PERSISTED
);

-- Custom build items
CREATE TABLE Order_items_b (
    item_id           BIGINT IDENTITY(1,1) PRIMARY KEY,
    order_id          BIGINT NOT NULL REFERENCES orders(order_id) ON DELETE CASCADE,
    build_id          BIGINT NOT NULL REFERENCES custom_builds(build_id),
    quantity          INT NOT NULL DEFAULT 1 CHECK (quantity > 0),
    unit_price        NUMERIC(10,2) NOT NULL CHECK (unit_price >= 0),
    total_price       AS (quantity * unit_price) PERSISTED
);