using AluraTunesData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aula7
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                var vendaMediana = Mediana(contexto.NotasFiscais.Select(ag => ag.Total));
                Console.WriteLine("Venda Mediana: R$ {0}", vendaMediana);

                vendaMediana = contexto.NotasFiscais.Mediana(ag => ag.Total);
                Console.WriteLine("Venda Mediana: R$ {0}", vendaMediana);
            }

            Console.ReadKey();
        }

        public static decimal Mediana(IQueryable<decimal> origem)
        {
            int contagem = origem.Count();
            var ordenado = origem.OrderBy(p => p);

            var elementoCentral_1 = ordenado.Skip((contagem - 1) / 2).First();
            var elementoCentral_2 = ordenado.Skip(contagem / 2).First();

            decimal mediana = (elementoCentral_1 + elementoCentral_2) / 2;

            return mediana;
        }

    }

    public static class Extensions
    {
        public static decimal Mediana<TSource>(this IQueryable<TSource> origem, Expression<Func<TSource, decimal>> selector)
        {
            int contagem = origem.Count();

            var funcSeletor = selector.Compile();
            var ordenado = origem
                .Select(selector)
                .OrderBy(x => x);

            var elementoCentral_1 = ordenado.Skip((contagem - 1) / 2).First();
            var elementoCentral_2 = ordenado.Skip(contagem / 2).First();

            decimal mediana = (elementoCentral_1 + elementoCentral_2) / 2;

            return mediana;
        }
    }
}
