using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeJourne.UnitTest.DomainServiceAuthTest.DataTest
{
    public class DomainServiceAuthData : TheoryData<string, string, bool>
    {
        public DomainServiceAuthData()
        {
            // Contraseña correcta
            Add("MySecurePassword123", "MySecurePassword123", true);
            // Contraseña incorrecta
            Add("MySecurePassword123", "WrongPassword", false);
            // Contraseña vacía
            Add("", "SomePassword", false);
            // Contraseña con caracteres especiales
            Add("P@ssw0rd!2023", "P@ssw0rd!2023", true);
            // Contraseña con espacios
            Add("  Spaces  ", "  Spaces  ", true);
        }
    }
}
