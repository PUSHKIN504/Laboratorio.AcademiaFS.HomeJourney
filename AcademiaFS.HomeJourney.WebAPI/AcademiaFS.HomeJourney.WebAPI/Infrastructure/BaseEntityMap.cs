using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

public abstract class BaseEntityMap<T> : IEntityTypeConfiguration<T> where T : class
{
    public abstract void ConfigureEntity(EntityTypeBuilder<T> builder);

    public void Configure(EntityTypeBuilder<T> builder)
    {
        //string entityName = typeof(T).Name;
        //if (entityName.EndsWith("s"))
        //{
        //    entityName = entityName.Substring(0, entityName.Length - 1);
        //}

        //string entityPrefix = entityName.Length >= 2 ? entityName.Substring(0, 2) : entityName;

        //PropertyInfo primaryKey = typeof(T).GetProperties()
        //    .FirstOrDefault(p => p.Name.Contains(entityPrefix, StringComparison.OrdinalIgnoreCase) &&
        //                         p.Name.EndsWith("id", StringComparison.OrdinalIgnoreCase));
        //if (primaryKey != null)
        //{
        //    builder.HasKey(primaryKey.Name);
        //}


        if (typeof(T).GetProperty("Activo") != null)
        {
            builder.Property("Activo").HasColumnType("bit").IsRequired();
        }

        if (typeof(T).GetProperty("Usuariocrea") != null)
        {
            builder.Property("Usuariocrea").IsRequired();
            builder.Property("Fechacrea").HasColumnType("datetime").IsRequired();
        }

        if (typeof(T).GetProperty("Usuariomodifica") != null)
        {
            builder.Property("Usuariomodifica").IsRequired(false);
            builder.Property("Fechamodifica").HasColumnType("datetime").IsRequired(false);
        }

        ConfigureEntity(builder); 
    }
}
