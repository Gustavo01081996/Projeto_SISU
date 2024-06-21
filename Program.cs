using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_AED_Final
{

    internal class Curso
    {
     
        private int codigoCurso;
        private string nomeCurso;
        private int qtdVagasCurso;

       
        FilaEspera filaDeEsperaCurso;
       
        List<Candidato> listaDeSelecionadosCurso;

       
        public Curso(int codigo, string nome, int quantVagas)
        {
            this.codigoCurso = codigo;
            this.nomeCurso = nome;
            this.qtdVagasCurso = quantVagas;
            filaDeEsperaCurso = new FilaEspera();
            listaDeSelecionadosCurso = new List<Candidato>();
        }

      
        public int CodigoCurso
        {
            get { return codigoCurso; }
            set { codigoCurso = value; }
        }
        public string NomeCurso
        {
            get { return nomeCurso; }
            set { nomeCurso = value; }
        }

        public int QtdVagasCurso
        {
            get { return qtdVagasCurso; }
            set { qtdVagasCurso = value; }
        }

        public FilaEspera FilaDeEsperaCurso
        {
            get { return filaDeEsperaCurso; }
            set { filaDeEsperaCurso = value; }
        }

        public List<Candidato> ListaDeSelecionadosCurso
        {
            get { return listaDeSelecionadosCurso; }
            set { listaDeSelecionadosCurso = value; }
        }

    }

    internal class FilaEspera
    {
       
        private Candidato[] array;
        int primeiro, ultimo;

       
        public FilaEspera()
        {
            array = new Candidato[11];
            primeiro = ultimo = 0;
        }

      
        public void Inserir(Candidato candidato)
        {
            if ((ultimo + 1) % array.Length == primeiro)
            {
                return;
            }

            array[ultimo] = candidato;
            ultimo = (ultimo + 1) % array.Length;
        }

        public void Remover()
        {
            if (primeiro == ultimo)
            {
                return;
            }

            primeiro = (primeiro + 1) % array.Length;
        }


      
        public void Mostrar(StreamWriter strWriter)
        {
            strWriter.WriteLine("Fila de Espera");

            if (primeiro == ultimo)
            {
                return;
            }

            int i = primeiro;


            while (i != ultimo)
            {
                strWriter.WriteLine($"{array[i].NomeCandidato} {array[i].MediaGeralCandidato:F2} {array[i].NotaRedacaoCandidato} {array[i].NotaMatematicaCandidato} {array[i].NotaLinguagensCandidato}");
                i = (i + 1) % array.Length;
            }

        }
    }

    internal class Universidade
    {
        
        Dictionary<int, Curso> listaDeCursos;

     
        public Universidade()
        {
            listaDeCursos = new Dictionary<int, Curso>();
        }

        
        public Dictionary<int, Curso> ListaDeCursos
        {
            get { return listaDeCursos; }
            set { listaDeCursos = value; }
        }

        
        public void InserirCursoNoDicionario(Curso curso)
        {
            
            listaDeCursos.Add(curso.CodigoCurso, curso);
        }

        
        public static void MergeSort(Candidato[] listaDeCandidatos, int esq, int dir)
        {
            if (esq < dir)
            {
                int meio = (esq + dir) / 2;
                MergeSort(listaDeCandidatos, esq, meio);
                MergeSort(listaDeCandidatos, meio + 1, dir);
                InterCalar(listaDeCandidatos, esq, meio, dir);
            }
        }

        public static void InterCalar(Candidato[] listaDeCandidatos, int esq, int meio, int dir)
        {
            int nEsq = meio - esq + 1;
            int nDir = dir - meio;
            int iEsq, iDir, i;
            Candidato[] candEsq = new Candidato[nEsq + 1];
            Candidato[] candDir = new Candidato[nDir + 1];

            for (int j = 0; j <= nEsq; j++)
            {
                candEsq[j] = new Candidato();
            }
            for (int j = 0; j <= nDir; j++)
            {
                candDir[j] = new Candidato();
            }

            candEsq[nEsq].MediaGeralCandidato = int.MinValue;
            candDir[nDir].MediaGeralCandidato = int.MinValue;

            for (iEsq = 0; iEsq < nEsq; iEsq++)
            {
                candEsq[iEsq] = listaDeCandidatos[esq + iEsq];
            }

            for (iDir = 0; iDir < nDir; iDir++)
            {
                candDir[iDir] = listaDeCandidatos[(meio + 1) + iDir];
            }

            for (iEsq = 0, iDir = 0, i = esq; i <= dir; i++)
            {
                if ((candEsq[iEsq].MediaGeralCandidato > candDir[iDir].MediaGeralCandidato) ||
                    (candEsq[iEsq].MediaGeralCandidato == candDir[iDir].MediaGeralCandidato && candEsq[iEsq].NotaRedacaoCandidato > candDir[iDir].NotaRedacaoCandidato) ||
                    (candEsq[iEsq].MediaGeralCandidato == candDir[iDir].MediaGeralCandidato && candEsq[iEsq].NotaRedacaoCandidato == candDir[iDir].NotaRedacaoCandidato && candEsq[iEsq].NotaMatematicaCandidato > candDir[iDir].NotaMatematicaCandidato) ||
                    (candEsq[iEsq].MediaGeralCandidato == candDir[iDir].MediaGeralCandidato && candEsq[iEsq].NotaRedacaoCandidato == candDir[iDir].NotaRedacaoCandidato && candEsq[iEsq].NotaMatematicaCandidato == candDir[iDir].NotaMatematicaCandidato && candEsq[iEsq].NotaLinguagensCandidato > candDir[iDir].NotaLinguagensCandidato))
                {
                    listaDeCandidatos[i] = candEsq[iEsq++];
                }
                else
                {
                    listaDeCandidatos[i] = candDir[iDir++];
                }
            }
        }

       
        public static bool VerificaVagaCurso(List<Candidato> listaDeSelecionadosTmp, int qtdVagasCurso)
        {
           
            if (listaDeSelecionadosTmp.Count < qtdVagasCurso)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       
        public static void Selecao(Candidato[] listaOrdenadaDeCandidatos, Dictionary<int, Curso> listaDeCursos)
        {
            Curso tmpOpcaoCurso1, tmpOpcaoCurso2;
            List<Candidato> tmpListaDeSelecionadosCurso1, tmpListaDeSelecionadosCurso2;
            FilaEspera tmpFilaDeEsperaCurso1, tmpFilaDeEsperaCurso2;

            int qtdVagasCurso1, qtdVagasCurso2;

            
            for (int i = 0; i < listaOrdenadaDeCandidatos.Length; i++)
            {
                
                tmpOpcaoCurso1 = listaDeCursos[listaOrdenadaDeCandidatos[i].Opcao1Candidato];

              
                tmpListaDeSelecionadosCurso1 = tmpOpcaoCurso1.ListaDeSelecionadosCurso;

                
                qtdVagasCurso1 = tmpOpcaoCurso1.QtdVagasCurso;

               
                if (VerificaVagaCurso(tmpListaDeSelecionadosCurso1, qtdVagasCurso1))
                {
                    
                    tmpListaDeSelecionadosCurso1.Add(listaOrdenadaDeCandidatos[i]);
                }
                
                else
                {
                   
                    tmpFilaDeEsperaCurso1 = tmpOpcaoCurso1.FilaDeEsperaCurso;
                   
                    tmpFilaDeEsperaCurso1.Inserir(listaOrdenadaDeCandidatos[i]);

                    tmpOpcaoCurso2 = listaDeCursos[listaOrdenadaDeCandidatos[i].Opcao2Candidato];

                   
                    tmpListaDeSelecionadosCurso2 = tmpOpcaoCurso2.ListaDeSelecionadosCurso;

                    
                    qtdVagasCurso2 = tmpOpcaoCurso2.QtdVagasCurso;

                   
                    if (VerificaVagaCurso(tmpListaDeSelecionadosCurso2, qtdVagasCurso2))
                    {
                       
                        tmpListaDeSelecionadosCurso2.Add(listaOrdenadaDeCandidatos[i]);
                    }
                   
                    else
                    {
                        
                        tmpFilaDeEsperaCurso2 = tmpOpcaoCurso2.FilaDeEsperaCurso;

                        tmpFilaDeEsperaCurso2.Inserir(listaOrdenadaDeCandidatos[i]);
                    }
                }

            }
        }

        public static double ProcuraMenorMedia(List<Candidato> listaOrdenadaDeCandidatos)
        {
            double menorMedia = double.MaxValue;

            foreach (Candidato candidato in listaOrdenadaDeCandidatos)
            {
                if (candidato.MediaGeralCandidato < menorMedia)
                {
                    menorMedia = candidato.MediaGeralCandidato;
                }
            }

            return menorMedia;
        }

      
        public static void ExibeListaDeSelecionados(List<Candidato> listaDeSelecionados, string nomeCurso, double notaDeCorte, StreamWriter arquivoDeSaida)
        {
           
            arquivoDeSaida.WriteLine($"{nomeCurso} {notaDeCorte:F2}");
            arquivoDeSaida.WriteLine("Selecionados");

        
            foreach (Candidato candidatosSelecionado in listaDeSelecionados)
            {
                arquivoDeSaida.WriteLine($"{candidatosSelecionado.NomeCandidato} {candidatosSelecionado.MediaGeralCandidato:F2} {candidatosSelecionado.NotaRedacaoCandidato} {candidatosSelecionado.NotaMatematicaCandidato} {candidatosSelecionado.NotaLinguagensCandidato}");
            }
        }

       
        public static void ExibeListasFilas(Dictionary<int, Curso> listaDeCursos, int codigoDoCurso, StreamWriter arquivoDeSaida)
        {
            
            Curso cursoEspecifico = listaDeCursos[codigoDoCurso];

            
            List<Candidato> listaDeSelecionados = cursoEspecifico.ListaDeSelecionadosCurso;

          
            FilaEspera filaDeEspera = cursoEspecifico.FilaDeEsperaCurso;

            
            string nomeCurso = cursoEspecifico.NomeCurso;

           
            double menorMediaCurso = ProcuraMenorMedia(listaDeSelecionados);

            
            ExibeListaDeSelecionados(listaDeSelecionados, nomeCurso, menorMediaCurso, arquivoDeSaida);
          
            filaDeEspera.Mostrar(arquivoDeSaida);

        
            arquivoDeSaida.WriteLine();
        }

    }

    internal class Candidato
    {
      
        private string nomeCandidato;
        private double notaRedacaoCandidato;
        private double notaMatematicaCandidato;
        private double notaLinguagensCandidato;
        private double mediaGeralCandidato;
        private int opcao1Candidato;
        private int opcao2Candidato;

        public Candidato(string nomeCandidato, double notaRedacaoCandidato, double notaMatematicaCandidato, double notaLinguagensCandidato, double mediaGeralCandidato, int opcao1Candidato, int opcao2Candidato)
        {

            this.nomeCandidato = nomeCandidato;
            this.notaRedacaoCandidato = notaRedacaoCandidato;
            this.notaMatematicaCandidato = notaMatematicaCandidato;
            this.notaLinguagensCandidato = notaLinguagensCandidato;
            this.mediaGeralCandidato = mediaGeralCandidato;
            this.opcao1Candidato = opcao1Candidato;
            this.opcao2Candidato = opcao2Candidato;

        }

    
        public string NomeCandidato
        {
            get { return nomeCandidato; }
            set { nomeCandidato = value; }
        }

        public double NotaRedacaoCandidato
        {
            get { return notaRedacaoCandidato; }
            set { notaRedacaoCandidato = value; }
        }

        public double NotaMatematicaCandidato
        {
            get { return notaMatematicaCandidato; }
            set { notaMatematicaCandidato = value; }
        }

        public double NotaLinguagensCandidato
        {
            get { return notaLinguagensCandidato; }
            set { notaLinguagensCandidato = value; }
        }


        public double MediaGeralCandidato
        {
            get { return mediaGeralCandidato; }
            set { mediaGeralCandidato = value; }
        }

        public int Opcao1Candidato
        {
            get { return opcao1Candidato; }
            set { opcao1Candidato = value; }
        }

        public int Opcao2Candidato
        {
            get { return opcao2Candidato; }
            set { opcao2Candidato = value; }
        }

    
        public Candidato()
        {
            this.nomeCandidato = "";
            this.notaRedacaoCandidato = 0;
            this.notaMatematicaCandidato = 0;
            this.notaLinguagensCandidato = 0;
            this.mediaGeralCandidato = 0;
            this.opcao1Candidato = 0;
            this.opcao2Candidato = 0;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            int qtdCursos, qtdCandidatos, codigoCurso, qtdVagasCurso;
            int indiceVetorCurso = 0;
            string nomeCurso, nomeCandidato;

            double notaRedacaoCandidato, notaMatematicaCandidato, notaLinguagensCandidato, mediaGeralCandidato;
            int opcao1Candidato, opcao2Candidato;
            int indiceVetorCandidato = 0;

            string leituraLinha;
            int contadorDeLinha = 0;
            string[] qtdCursosEcandidatos = new string[1];

            StreamReader arquivoEntrada = new StreamReader("C:\\Users\\conta\\Downloads\\entrada.txt", Encoding.UTF8);

            leituraLinha = arquivoEntrada.ReadLine();


            qtdCursosEcandidatos = leituraLinha.Split(';');

            qtdCursos = int.Parse(qtdCursosEcandidatos[0]);
            qtdCandidatos = int.Parse(qtdCursosEcandidatos[1]);

            Curso[] listaDeCursos = new Curso[qtdCursos];

            Candidato[] listaDeCandidatos = new Candidato[qtdCandidatos];

            Universidade stark = new Universidade();

        
            string[] cursos = new string[qtdCursos];

            string[] candidatos = new string[qtdCandidatos];

            while ((leituraLinha = arquivoEntrada.ReadLine()) != null)
            {
   
                if (contadorDeLinha < qtdCursos)
                {

                    cursos = leituraLinha.Split(';');
                    codigoCurso = int.Parse(cursos[0]);
                    nomeCurso = cursos[1];
                    qtdVagasCurso = int.Parse(cursos[2]);


                    listaDeCursos[indiceVetorCurso] = new Curso(codigoCurso, nomeCurso, qtdVagasCurso);

                    stark.InserirCursoNoDicionario(listaDeCursos[indiceVetorCurso]);


                    indiceVetorCurso++;
                }

                else
                {

                    candidatos = leituraLinha.Split(';');
                    nomeCandidato = candidatos[0];
                    notaRedacaoCandidato = double.Parse(candidatos[1]);
                    notaMatematicaCandidato = double.Parse(candidatos[2]);
                    notaLinguagensCandidato = double.Parse(candidatos[3]);


                    mediaGeralCandidato = (notaRedacaoCandidato + notaMatematicaCandidato + notaLinguagensCandidato) / 3;

                    opcao1Candidato = int.Parse(candidatos[4]);
                    opcao2Candidato = int.Parse(candidatos[5]);


                    listaDeCandidatos[indiceVetorCandidato] = new Candidato(nomeCandidato, notaRedacaoCandidato, notaMatematicaCandidato, notaLinguagensCandidato, mediaGeralCandidato, opcao1Candidato, opcao2Candidato);


                    indiceVetorCandidato++;
                }

                contadorDeLinha++;
            }
            arquivoEntrada.Close();


            Universidade.MergeSort(listaDeCandidatos, 0, listaDeCandidatos.Length - 1);

            Universidade.Selecao(listaDeCandidatos, stark.ListaDeCursos);


            try
            {

                StreamWriter arquivoDeSaida = new StreamWriter("C:\\Users\\conta\\Downloads\\saida.txt", true, Encoding.UTF8);

                foreach (KeyValuePair<int, Curso> item in stark.ListaDeCursos)
                {
   
                    codigoCurso = item.Key;
                    Universidade.ExibeListasFilas(stark.ListaDeCursos, codigoCurso, arquivoDeSaida);
                }

                arquivoDeSaida.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            Console.ReadKey();  

        }
    }
}
