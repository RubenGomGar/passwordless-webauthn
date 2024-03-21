// See https://aka.ms/new-console-template for more information
// Paso 1: Seleccionar dos números primos
using System.Text;

// Paso 1: Seleccionar dos números primos
int p = 17;
int q = 23;

// Paso 2: Calcular n
int n = p * q;

// Paso 3: Calcular φ(n)
int phi = (p - 1) * (q - 1);

// Paso 4: Elegir un número co-primo (e)
int e = 5;

// Paso 5: Calcular la clave privada (d) => (d * e) mod φ(n) = 1
int d = CalculatePrivateKey(e, phi);

// Mostrar claves públicas y privadas
Console.WriteLine($"Clave pública (n, e): ({n} , {e})");
Console.WriteLine($"Clave privada (n, d): ({n} , {d})");

// Mensaje a encriptar
string message = "HELLO NetCoreConf 2024";
Console.WriteLine("Mensaje original: " + message);

// Convertir el mensaje a números ASCII
int[] asciiMessage = ConvertToASCII(message);

// Paso 6: Encriptar el mensaje C(m) = m^e mod n
int[] encryptedMessage = Encrypt(asciiMessage, e, n);

Console.WriteLine("Mensaje encriptado: " + string.Join(", ", encryptedMessage));

// Paso 7: Desencriptar el mensaje D(C) = C^d mod n
int[] decryptedMessage = Decrypt(encryptedMessage, d, n);

// Convertir los números ASCII de nuevo a caracteres
string decryptedText = ConvertToString(decryptedMessage);

Console.WriteLine("Mensaje desencriptado: " + decryptedText);

Console.WriteLine("Conclusión: El sistema RSA utilizan exponenciación modular, que es una operación que tiene una propiedad inversa, lo que permite deshacer la operación original.");

Console.WriteLine("Presiona una tecla para salir...");
Console.ReadKey();
static int CalculatePrivateKey(int e, int phi)
{
    for (int d = 1; d < phi; d++)
    {
        if ((d * e) % phi == 1)
        {
            return d;
        }
    }
    return 0; // Error
}

static int[] ConvertToASCII(string text)
{
    int[] asciiArray = new int[text.Length];
    for (int i = 0; i < text.Length; i++)
    {
        asciiArray[i] = (int)text[i];
    }
    return asciiArray;
}

static string ConvertToString(int[] asciiArray)
{
    StringBuilder sb = new StringBuilder();
    foreach (int asciiCode in asciiArray)
    {
        sb.Append((char)asciiCode);
    }
    return sb.ToString();
}

static int[] Encrypt(int[] message, int e, int n)
{
    int[] encryptedMessage = new int[message.Length];
    for (int i = 0; i < message.Length; i++)
    {
        encryptedMessage[i] = ModPow(message[i], e, n);
    }
    return encryptedMessage;
}

static int ModPow(int baseValue, int exponent, int modulus)
{
    int result = 1;
    while (exponent > 0)
    {
        if (exponent % 2 == 1)
        {
            result = (result * baseValue) % modulus;
        }
        baseValue = (baseValue * baseValue) % modulus;
        exponent /= 2;
    }
    return result;
}

static int[] Decrypt(int[] encryptedMessage, int d, int n)
{
    int[] decryptedMessage = new int[encryptedMessage.Length];
    for (int i = 0; i < encryptedMessage.Length; i++)
    {
        decryptedMessage[i] = ModPow(encryptedMessage[i], d, n);
    }
    return decryptedMessage;
}