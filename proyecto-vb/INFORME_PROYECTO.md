# INFORME DEL PROYECTO: SISTEMA DE GESTIÓN FERREE-LARA
## Ingeniería en Computación - Proyecto de Desarrollo de Software

---

## A) DESCRIPCIÓN DE LA EMPRESA

### Nombre Comercial
**Ferree-Lara** — Ferretería y Soluciones Técnicas

### Giro del Negocio
Ferree-Lara es una ferretería dedicada a la comercialización de herramientas manuales y eléctricas, materiales de construcción, artículos de plomería, electricidad, cerrajería y pinturas. Atiende tanto a profesionales de la construcción como al público general que requiere productos para mantenimiento y mejoras del hogar.

### Misión
Proveer a nuestros clientes materiales, herramientas y soluciones técnicas de la más alta calidad, ofreciendo un servicio ágil, asesoría especializada y precios competitivos que contribuyan al éxito de sus proyectos de construcción, reparación y mejora del hogar.

### Visión
Consolidarnos como la ferretería de referencia en la región, reconocida por la amplitud de nuestro catálogo, la excelencia en el servicio al cliente y la adopción de tecnología que optimice nuestros procesos internos, garantizando una experiencia de compra confiable y moderna.

### Valores
- **Calidad:** Ofrecemos solo productos y marcas con respaldo y garantía.
- **Honestidad:** Transparencia en precios, promociones y asesoría técnica.
- **Servicio al Cliente:** Atención personalizada y orientación profesional.
- **Innovación:** Incorporamos herramientas tecnológicas para mejorar nuestros procesos operativos.

### Problemática Identificada
Actualmente, Ferree-Lara gestiona sus operaciones de venta e inventario mediante procesos manuales (registro en cuadernos, hojas de cálculo sin respaldo, y conteo físico de stock). Esto genera los siguientes problemas:

1. **Errores en el control de inventario:** Discrepancias entre el stock real y el registrado, provocando ventas de productos agotados o sobrecompras.
2. **Lentitud en el punto de venta:** El proceso de facturación manual genera colas y tiempos de espera innecesarios.
3. **Falta de trazabilidad:** No se cuenta con un historial digital de ventas que permita análisis de tendencias o auditorías.
4. **Riesgo de pérdida de datos:** La información en papel o archivos locales sin respaldo está expuesta a extravíos, deterioro o errores humanos.
5. **Imposibilidad de generar reportes:** Sin un sistema centralizado, la gerencia no puede tomar decisiones basadas en datos concretos de ventas y rotación de productos.

### Solución Propuesta
El sistema **Ferree-Lara** es una aplicación de escritorio desarrollada en Visual Basic .NET (Windows Forms) con base de datos local SQLite, diseñada para automatizar y optimizar los procesos de ventas e inventario de la ferretería. El sistema permite:
- Control de acceso por roles (Administrador y Cajero).
- Gestión de catálogo de productos e inventario en tiempo real.
- Registro y administración de clientes.
- Punto de venta (POS) con cálculo automático de totales, impuestos y cambio.
- Generación de facturas digitales.
- Descuento automático de stock al procesar cada venta.

---

## B) EXPLICACIÓN DETALLADA DE LOS PROCESOS LÓGICOS DEL PROGRAMA

### B.1) Arquitectura General del Sistema

El sistema Ferree-Lara sigue una arquitectura de **dos capas** (presentación + datos) organizada en los siguientes componentes:

```
┌─────────────────────────────────────────────────────────┐
│                  CAPA DE PRESENTACIÓN                   │
│  (Formularios Windows Forms - Interfaz de Usuario)      │
│                                                         │
│  ┌──────────┐  ┌──────────────┐  ┌────────────────┐    │
│  │ frmLogin │→ │ frmPrincipal │→ │ frmMantenimiento│   │
│  └──────────┘  └──────────────┘  └────────────────┘    │
│                       │                                  │
│                       ├──────→ ┌───────────┐            │
│                       │        │ frmVentas │            │
│                       │        └───────────┘            │
│                       │              │                   │
│                       │              ▼                   │
│                       │        ┌────────────┐           │
│                       │        │ frmFactura │           │
│                       │        └────────────┘           │
│                       │                                  │
│  ┌──────────────────────────────────────────────────┐   │
│  │  Módulos Compartidos:                            │   │
│  │  • DatabaseHelper (Acceso a datos)               │   │
│  │  • SesionActual   (Estado de la sesión)          │   │
│  └──────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────┘
                          │
                          ▼
┌─────────────────────────────────────────────────────────┐
│                   CAPA DE DATOS                         │
│         Base de datos local SQLite                      │
│                                                         │
│  ┌──────────┐  ┌──────────┐  ┌───────────┐             │
│  │ Usuarios │  │ Clientes │  │ Productos │             │
│  └──────────┘  └──────────┘  └───────────┘             │
│  ┌──────────┐  ┌────────────────┐                      │
│  │  Ventas  │  │ Detalle_Ventas │                      │
│  └──────────┘  └────────────────┘                      │
└─────────────────────────────────────────────────────────┘
```

### B.2) Proceso de Autenticación (frmLogin)

El proceso de inicio de sesión es la puerta de entrada al sistema y sigue la siguiente lógica:

1. **Inicialización:** Al cargar el formulario, el sistema ejecuta `DatabaseHelper.InicializarBaseDatos()`, que crea las tablas si no existen y puebla los datos iniciales (usuarios de prueba, un cliente "Consumidor Final" y un catálogo de productos de ejemplo).

2. **Validación de campos:** Antes de consultar la base de datos, se verifica que ambos campos (usuario y contraseña) no estén vacíos. Si alguno lo está, se muestra un mensaje descriptivo y se posiciona el cursor en el campo faltante.

3. **Autenticación segura:**
   - La contraseña ingresada se transforma mediante un hash SHA-256 antes de compararla con la almacenada en la base de datos.
   - Se ejecuta una consulta SQL parametrizada (prevención de inyección SQL) que busca un registro en la tabla `Usuarios` donde coincidan el nombre de usuario, el hash de la contraseña y el campo `Activo = 1`.
   - Si existe coincidencia, se leen los datos del usuario (ID, nombre completo, rol) y se almacenan en el módulo `SesionActual`.

4. **Control de intentos fallidos:** Se implementa un contador que permite un máximo de 5 intentos. Al superar el límite, el sistema se bloquea hasta reiniciar la aplicación, protegiendo contra ataques de fuerza bruta.

5. **Acceso concedido:** Tras un login exitoso, el formulario se oculta y se abre `frmPrincipal` como diálogo modal. Al cerrar el menú principal, se limpia la sesión, se reinicia el contador de intentos y se muestra el login nuevamente.

### B.3) Proceso de Gestión de Catálogo e Inventario (frmMantenimiento)

Este módulo permite las operaciones CRUD (Crear, Leer, Actualizar, Eliminar) sobre dos entidades principales:

**Gestión de Productos:**
1. **Registro:** Se capturan código, nombre, descripción, categoría, precio de venta, precio de costo, stock inicial y stock mínimo. Se valida que los precios sean numéricos positivos y que el código no exista previamente.
2. **Búsqueda:** Se permite buscar por código, nombre o categoría. Los resultados se muestran en un DataGridView.
3. **Edición:** Al seleccionar un producto del DataGridView, se cargan sus datos en los campos del formulario para su modificación. Se validan los cambios antes de guardar.
4. **Desactivación lógica:** En lugar de eliminar registros (lo que afectaría la integridad referencial con las ventas históricas), se marca el producto como inactivo (`Activo = 0`).

**Gestión de Clientes:**
1. **Registro:** Se capturan nombre, apellido, teléfono, correo, dirección y cédula/identificación. Se valida que la cédula no esté duplicada.
2. **Búsqueda y edición:** Funcionamiento análogo al de productos.

### B.4) Proceso de Venta (frmVentas) — Proceso Principal

El punto de venta es el proceso más crítico del sistema y opera de la siguiente manera:

1. **Selección de cliente:** El cajero puede seleccionar un cliente registrado de un ComboBox o utilizar el cliente genérico "Consumidor Final" (que es la opción por defecto).

2. **Búsqueda y adición de productos al carrito:**
   - El cajero busca productos por código o nombre.
   - Al encontrar un producto, verifica que tenga stock disponible (`Stock > 0`).
   - Selecciona la cantidad deseada (validando que no exceda el stock disponible).
   - El producto se agrega a un DataGridView que funciona como "carrito de compras", calculando automáticamente el subtotal de línea (`PrecioUnitario × Cantidad`).
   - Si se intenta agregar un producto que ya está en el carrito, se suma la cantidad nueva a la existente.

3. **Cálculo de totales:**
   - **Subtotal:** Suma de todos los subtotales de línea del carrito.
   - **Impuesto (IVA 15%):** Se calcula como `Subtotal × 0.15`.
   - **Total:** `Subtotal + Impuesto`.
   - Estos valores se recalculan automáticamente cada vez que se modifica el carrito.

4. **Procesamiento del pago:**
   - El cajero ingresa el monto entregado por el cliente.
   - El sistema valida que el monto sea mayor o igual al total.
   - Se calcula el cambio a devolver: `MontoPagado - Total`.

5. **Confirmación y registro de la venta (transacción atómica):**
   - Se genera un número de factura único con el formato `FT-YYYYMMDD-XXXX`.
   - Se inicia una transacción en la base de datos para garantizar la integridad.
   - Se inserta el registro de encabezado en la tabla `Ventas`.
   - Se inserta cada línea del carrito en la tabla `Detalle_Ventas`.
   - Se descuenta el stock de cada producto vendido en la tabla `Productos` (`Stock = Stock - CantidadVendida`).
   - Si todo es exitoso, se confirma la transacción (COMMIT); si ocurre algún error, se revierte (ROLLBACK).

6. **Generación de factura:** Se abre `frmFactura` con el ID de la venta procesada para mostrar el comprobante al cliente.

### B.5) Proceso de Generación de Factura (frmFactura)

1. Recibe el ID de la venta procesada desde `frmVentas`.
2. Consulta la base de datos para obtener el encabezado de la venta (cliente, fecha, totales, método de pago).
3. Consulta los detalles de la venta (productos, cantidades, precios unitarios, subtotales).
4. Presenta la información en formato de factura formal con los datos fiscales de la empresa.

### B.6) Modelo de Datos (Diagrama Entidad-Relación)

```
┌─────────────┐       ┌──────────────┐       ┌──────────────┐
│   Usuarios  │       │   Clientes   │       │  Productos   │
├─────────────┤       ├──────────────┤       ├──────────────┤
│*UsuarioId   │       │*ClienteId    │       │*ProductoId   │
│ NombreUsuario│      │ Nombre       │       │ Codigo       │
│ Contrasena  │       │ Apellido     │       │ Nombre       │
│ NombreCompleto│     │ Telefono     │       │ Descripcion  │
│ Rol         │       │ Correo       │       │ Categoria    │
│ Activo      │       │ Direccion    │       │ PrecioVenta  │
│ FechaCreacion│      │ Cedula       │       │ PrecioCosto  │
└──────┬──────┘       │ FechaRegistro│       │ Stock        │
       │              └──────┬───────┘       │ StockMinimo  │
       │                     │               │ Activo       │
       │    ┌────────────────┘               │ FechaRegistro│
       │    │                                └──────┬───────┘
       ▼    ▼                                       │
┌──────────────────┐                                │
│      Ventas      │                                │
├──────────────────┤                                │
│*VentaId          │                                │
│ NumeroFactura    │         ┌──────────────────┐   │
│ ClienteId (FK)   │────────→│ Detalle_Ventas   │←──┘
│ UsuarioId (FK)   │         ├──────────────────┤
│ FechaVenta       │         │*DetalleId        │
│ Subtotal         │         │ VentaId (FK)     │
│ Impuesto         │         │ ProductoId (FK)  │
│ Total            │         │ Cantidad         │
│ MontoPagado      │         │ PrecioUnitario   │
│ Cambio           │         │ SubtotalLinea    │
│ MetodoPago       │         └──────────────────┘
│ Estado           │
└──────────────────┘
```

**Relaciones:**
- `Ventas.ClienteId` → `Clientes.ClienteId` (Muchas ventas pueden pertenecer a un cliente)
- `Ventas.UsuarioId` → `Usuarios.UsuarioId` (Muchas ventas pueden ser registradas por un usuario)
- `Detalle_Ventas.VentaId` → `Ventas.VentaId` (Una venta tiene muchos detalles)
- `Detalle_Ventas.ProductoId` → `Productos.ProductoId` (Un producto puede aparecer en muchos detalles)

### B.7) Seguridad del Sistema

- **Autenticación:** Contraseñas almacenadas como hash SHA-256 (nunca en texto plano).
- **Autorización por roles:** El rol del usuario determina las acciones permitidas (el Administrador tiene acceso completo; el Cajero solo a ventas y consultas).
- **Prevención de inyección SQL:** Todas las consultas utilizan parámetros (`@parametro`) en lugar de concatenación de cadenas.
- **Control de intentos de login:** Máximo 5 intentos fallidos antes de bloquear el acceso.
- **Integridad transaccional:** Las operaciones de venta se ejecutan dentro de transacciones SQL (BEGIN/COMMIT/ROLLBACK) para garantizar consistencia.

---