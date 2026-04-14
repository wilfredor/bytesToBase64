# bytesToBase64

A from-scratch Base64 encoding implementation written in C#. This project demonstrates the low-level mechanics of the Base64 algorithm — converting raw byte arrays into Base64-encoded strings — without relying on any built-in library methods.

## How It Works

The encoder follows the standard Base64 specification:

1. **Input** — Takes a byte array whose length is a multiple of 3
2. **Concatenation** — Groups every 3 bytes (24 bits) together
3. **Splitting** — Divides each 24-bit group into four 6-bit segments
4. **Mapping** — Maps each 6-bit value to a character in the Base64 alphabet (`A-Z`, `a-z`, `0-9`, `/`, `+`)
5. **Output** — Returns the encoded string

## Usage

```csharp
byte[] data = new byte[] { 72, 101, 108 }; // "Hel"
string encoded = Base64.Encode(data);
Console.WriteLine(encoded); // "SGVs"
```

> **Note:** The input byte array length must be a multiple of 3. Padding (`=`) is not implemented.

## API

| Method | Description |
|--------|-------------|
| `Base64.Encode(byte[] source)` | Encodes a byte array into a Base64 string |

## Tech Stack

- **Language:** C#
- **Framework:** .NET

## Purpose

This is an educational project meant to illustrate how Base64 encoding works at the bit level, useful for understanding data serialization, binary-to-text encoding schemes, and the internals behind `Convert.ToBase64String`.

## Author

Wilfredo Rodríguez — wilfredor@gmail.com

## License

This project is open source.
