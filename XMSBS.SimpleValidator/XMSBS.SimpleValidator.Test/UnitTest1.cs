using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using XMSBS.SimpleValidator.Exceptions;
using XMSBS.SimpleValidator.Interfaces;
using XMSBS.SimpleValidator.Results;

namespace XMSBS.SimpleValidator.Test
{
    public class Tests
    {

        public class prueba
        {
            public string nombre { get; set; }
            public int orden { get; set; }
            public string email { get; set; }
        }

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

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            var prueba = new prueba()
            {
                nombre = "otra cosa",
                orden = 1,
                email = "cchacon@xms.cl"
            };

            try
            {
               SimpleValidator<prueba> validator = new SimpleValidator<prueba>(prueba);

                validator.IsNotNullOrEmpty(p => p.nombre, "El campo nombre no puede estar vacio")
                    .IsNotZero(p => p.orden, "El campo orden debe ser uno")
                    .IsEmail(p => p.email, "El formato del mail no es correcto")
                    .AddBusinessRules(new MyRule("otra cosa"))
                    .ExecuteDataValidations()
                    .ExecuteBusinessRulesValidation();
            }
            catch (DataValidationException exD)
            {
                Assert.Fail(string.Join(",", exD.Messages));
            }
            catch (BusinessException exB)
            {
                Assert.Fail(string.Join(",", exB.Messages));
            }
        }
    }
}