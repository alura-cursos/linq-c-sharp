using AluraTunesData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula3
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                var query = from g in contexto.Generos
                            select g;

                foreach (var genero in query)
                {
                    Console.WriteLine("{0}\t{1}", genero.GeneroId, genero.Nome);
                }

                var faixaEgenero = from g in contexto.Generos
                                   join f in contexto.Faixas
                                    on g.GeneroId equals f.GeneroId
                                   select new { f, g };

                faixaEgenero = faixaEgenero.Take(10);

                contexto.Database.Log = Console.WriteLine;

                Console.WriteLine();
                foreach (var item in faixaEgenero)
                {
                    Console.WriteLine("{0}\t{1}", item.f.Nome, item.g.Nome);
                }
            }

            Console.ReadKey();
        }
    }
}
