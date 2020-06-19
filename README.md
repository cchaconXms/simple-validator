# XMSBS.SimpleValidator


## Tipos de Validaciones

#### DataValidation

Podras definir la validación que necesites y el resultado como mensaje de error.

```c#
SimpleValidator<prueba> validator = new SimpleValidator<prueba>(objetoPrueba);

validator.AddDataValidation(p => p.orden > 20 && p.orden < 50 ? "" : "El orden se encuentra fuera del rango permitido")
         .ExecuteDataValidations();
```

#### BusinessRules

Podras definir reglas de negocio de forma aislada, la cual te permitirá reutilizar en distintas otras funcionalidades, para ellos deberas implementar la interfaz **IRules**, podras ejecutar reglas sincronas o asincronas y como respuesta esperada debera usar la clase **RuleResult**

Cabe destacar que usando ServiceCollector de .NET Core podran definir sus reglas en el startup.cs, de esta manera podran inyectar en las dependencias que necesiten.  

```c#
public interface IRule<in T> where T : class
{
    Task<RuleResult> ExecuteAsync(T entity);
    RuleResult Execute(T entity);
}
```
```c#

//CLASE DE EJEMPLO
public class MyRule : IRule<prueba>
{
   private readonly string _nombre;

   public MyRule(string Inyection)
   {
      _nombre = Inyection;
   }

   public RuleResult Execute(prueba entity)
   {
      List<string> messages = new List<string>();
      RuleResult result = null;

      if (entity.nombre == _nombre)
      {
         result = new RuleResult()
         {
           IsSuccess = true
         };
      }
      else
      {
         messages.Add("El nombre no coincide con el parametro");
      }

      if (messages.Any())
      {
          result = new RuleResult()
          {
             IsSuccess = false,
             ErrorMessages = messages
          };
      }
      else
      {
         result = new RuleResult()
         {
            IsSuccess = true
         };
      }

      return result;
  }

   public Task<RuleResult> ExecuteAsync(prueba entity)
   {
       throw new System.NotImplementedException();
   }
}
```

```c#
SimpleValidator<prueba> validator = new SimpleValidator<prueba>(objetoPrueba);

validator.AddBusinessRules(MyRuleClass)
         .ExecuteBusinessRulesValidation();
```

#### Integer Validations

Podras validar propiedades de tipo *int* para cumplan con criterios definidos, deberas validar la propiedad usando expresion lambda, seguido de su mensaje de error.

Metodos de validacion:
- IsNotZero
- Is
- IsGreaterThan
- IsLessThan
- IsBetween

```c#
SimpleValidator<prueba> validator = new SimpleValidator<prueba>(objetoPrueba);

validator.IsNotZero(p => p.orden, "El numero de orden no debe ser cero")
         .ExecuteDataValidations();
```

#### Regex Validations

Podras validar propiedades de tipo *string* para que cumplan con criterios definidos en las expresiones regulares, deberas validar la propiedad usando expresion lambda, seguido de su mensaje de error.

Métodos de validación;
- IsEmail
- IsPassword
- IsRegex

```c#
SimpleValidator<prueba> validator = new SimpleValidator<prueba>(objetoPrueba);

validator.IsEmail(p => p.email, "El formato del mail no es correcto")
         .ExecuteDataValidations();
```

#### String Validations

Podras validar propiedades de tipo *string* para que cumplan con los criterios definidos, deberas validar la propiedad usando expresion lambda, seguido de su mensaje de error.

Métodos de validación:
- IsNotNullOrEmpty
- IsNotNullOrWhiteSpace
- IsMaxLength
- IsMinLength

```c#
SimpleValidator<prueba> validator = new SimpleValidator<prueba>(objetoPrueba);

validator.IsNotNullOrEmpty(p => p.nombre, "El campo nombre no puede estar vacio")
         .ExecuteDataValidations();
```

License
----

Apache-2-0
