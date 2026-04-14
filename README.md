# bytesToBase64

Base64 encoder from scratch in C#. No `Convert.ToBase64String` — manual bit manipulation.

## Algorithm

1. Group every 3 bytes (24 bits)
2. Split into four 6-bit segments
3. Map each 6-bit value to Base64 alphabet (`A-Z`, `a-z`, `0-9`, `/`, `+`)

Input length must be multiple of 3. Padding (`=`) not implemented.

## Usage

```csharp
byte[] data = new byte[] { 72, 101, 108 }; // "Hel"
string encoded = Base64.Encode(data);
Console.WriteLine(encoded); // "SGVs"
```

## API

| Method | Description |
|--------|-------------|
| `Base64.Encode(byte[] source)` | Encode byte array to Base64 string |
