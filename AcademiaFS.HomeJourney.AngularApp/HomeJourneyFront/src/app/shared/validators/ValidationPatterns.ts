// validation-patterns.ts
export class ValidationPatterns {
    // Solo números (por ejemplo, para el DNI)
    public static readonly NUMERIC_ONLY = /^[0-9]+$/;
    
    // Ejemplo para email (puedes ajustar según tu necesidad)
    public static readonly EMAIL = /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/;
    
    // Puedes agregar más patrones según sea necesario:
    // public static readonly ALPHANUMERIC = /^[a-zA-Z0-9]+$/;
  }
  