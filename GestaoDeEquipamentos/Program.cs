using System;
using System.Collections;
using System.Globalization;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;

namespace GestaoDeEquipamentos
{
    internal class Program
    {
        static string nomeEquipamento = "";
        static string fabricanteEquipamento = "";
        static int idEquipamentos = 0;
        static string numeroDeSerieEquipamento = "";
        static double precoEquipamento = 0;
        static DateTime dataFabricacaoEquipamento = DateTime.UtcNow;

        static int idChamados = 0;
        static string títuloChamados = "";
        static string descriçãoChamados = "";
        static int EquipamentosIdChamados = 0;
        static DateTime dataDeAberturaChamados = DateTime.UtcNow;

        static ArrayList listaIdChamados = new ArrayList();
        static ArrayList listaTitulosChamados = new ArrayList();
        static ArrayList listaDescricaoChamados = new ArrayList();
        static ArrayList listaIdDoEquipamentoNosChamados = new ArrayList();
        static ArrayList listaDataAberturaChamados = new ArrayList();

        static ArrayList listaIdEquipamentos = new ArrayList();
        static ArrayList listaPrecoEquipamentos = new ArrayList();
        static ArrayList listaNomeEquipamentos = new ArrayList();
        static ArrayList listaNumeroDeSerieEquipamentos = new ArrayList();
        static ArrayList listaDataEquipamentos = new ArrayList();
        static ArrayList listaFabricanteEquipamentos = new ArrayList();

        static void Main(string[] args)
        {
            string resposta = "";
            while (resposta.ToUpper() != "S")
            {
                resposta = MostraMenuInicial();
                if (resposta == "1")
                {
                    CRUD_Equipamentos(resposta);
                    resposta = "";
                    continue;
                }
                if (resposta == "2")
                {
                    CRUD_Chamados(resposta);
                    resposta = "";
                    continue;
                }
            }
        }

        static string MostraMenuInicial()
        {
            string resposta;
            Console.WriteLine("Oque deseja fazer: ");
            Console.WriteLine(" 1- Equipamentos\n 2- Chamados\n S para Sair");
            resposta = Console.ReadLine();
            return resposta;
        }

        #region Equipamentos

        static string CRUD_Equipamentos(string resposta)
        {
            while (resposta.ToUpper() != "S")
            {
                Console.Clear();
                resposta = MostraMenuEquipamentos();
                if (resposta == "1")
                {
                    Console.Clear();
                    AdicionaEquipamentos(resposta);
                    resposta = "";
                    continue;
                }
                if (resposta == "2")
                {
                    Console.Clear();
                    MostraTodosOsEquipamentos();
                    resposta = "";
                    continue;
                }
                if (resposta == "3")
                {
                    Console.Clear();
                    ModificaUmEquipamentoEscolhido(resposta);
                    resposta = "";
                    continue;
                }
                if (resposta == "4")
                {
                    Console.Clear();
                    DeletaEquipamentos(resposta);
                    resposta = "";
                    continue;
                }

            }

            return resposta;
        }

        static string MostraMenuEquipamentos()
        {
            string resposta;
            Console.WriteLine("Menu Equipamentos: ");
            Console.WriteLine(" 1- Adicionar Equipamentos\n 2- Visualizar Todos os Equipamentos\n 3- Editar Equipamentos\n 4- Excluir Equipamentos\n S para Sair");
            resposta = Console.ReadLine();
            return resposta;
        }

        static void DeletaEquipamentos(string resposta)
        {
            while (resposta.ToUpper() != "S")
            {
                if (listaIdEquipamentos.Count == 0)
                {
                    MensagemDeErro("Nao existem valores na lista");
                }
                else
                {
                    MostraTodosOsEquipamentos();
                    Console.WriteLine("Id de quem deseja deletar: ");
                    int idParaDeletar = Convert.ToInt32(Console.ReadLine());
                    bool IdExiste = false;
                    for (int i = 0; i < listaIdEquipamentos.Count; i++)
                    {
                        if (listaIdEquipamentos[i].Equals(idParaDeletar))
                        {
                            for (int j = 0; j < listaIdDoEquipamentoNosChamados.Count; j++)
                            {
                                if (listaIdEquipamentos[j] == listaIdDoEquipamentoNosChamados[i])
                                {
                                    Console.WriteLine("Equipamento possui chamado cadastrado");
                                    continue;
                                }
                                else
                                {
                                    RemoveDasListasEquipamentos(i);
                                    IdExiste = true;
                                }

                            }

                        }

                    }
                    if (IdExiste == false)
                    {
                        MensagemDeErro("Id nao existe...");
                    }
                }
                Console.Write("Sair: ");
                resposta = Console.ReadLine();
            }
        }

        static void RemoveDasListasEquipamentos(int i)
        {
            listaIdEquipamentos.RemoveAt(i);
            listaNomeEquipamentos.RemoveAt(i);
            listaPrecoEquipamentos.RemoveAt(i);
            listaDataEquipamentos.RemoveAt(i);
            listaNumeroDeSerieEquipamentos.RemoveAt(i);
            listaFabricanteEquipamentos.RemoveAt(i);
        }

        static void ModificaUmEquipamentoEscolhido(string resposta)
        {
            while (resposta.ToUpper() != "S")
            {
                if (listaIdEquipamentos.Count == 0)
                {
                    MensagemDeErro("Nao existem valores na lista");
                }
                else
                {
                    MostraTodosOsEquipamentos();
                    Console.WriteLine("Id de quem deseja modificar;");
                    int idParaModificar = Convert.ToInt32(Console.ReadLine());
                    for (int i = 0; i < listaIdEquipamentos.Count; i++)
                    {
                        if (listaIdEquipamentos[i].Equals(idParaModificar))
                        {
                            PegadadosDoUsuarioEquipamentos();
                            if (nomeEquipamento.Length < 6)
                            {
                                MensagemDeErro("O Nome deve ter no mínimo 6 caracters");
                                continue;
                            }
                            ModificaAsListasEquipamentos(i);
                            MostraSucessoAoUsuarioEquipamentos("Modificado com Sucesso!");
                        }
                        else
                        {
                            MensagemDeErro("Id Inválido");
                            break;
                        }
                    }
                }
                Console.Write("Sair: ");
                resposta = Console.ReadLine();
            }
        }

        static void ModificaAsListasEquipamentos(int i)
        {
            listaNomeEquipamentos[i] = nomeEquipamento;
            listaPrecoEquipamentos[i] = precoEquipamento;
            listaDataEquipamentos[i] = dataFabricacaoEquipamento;
            listaNumeroDeSerieEquipamentos[i] = numeroDeSerieEquipamento;
            listaFabricanteEquipamentos[i] = fabricanteEquipamento;
        }

        static void AdicionaEquipamentos(string resposta)
        {
            while (resposta.ToUpper() != "S")
            {
                PegadadosDoUsuarioEquipamentos();
                if (nomeEquipamento.Length < 6)
                {
                    MensagemDeErro("O Nome deve ter no mínimo 6 caracters");
                    continue;
                }
                AdicionaNasListasEquipamentos();
                MostraSucessoAoUsuarioEquipamentos("Adicionado com Sucesso!");
                Console.Write("Sair: ");
                resposta = Console.ReadLine();
            }
        }

        static void MensagemDeErro(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }

        static void MostraTodosOsEquipamentos()
        {
            if (listaIdEquipamentos.Count == 0)
            {
                MensagemDeErro("Nao existem valores na lista");
            }
            else
            {
                Console.WriteLine(" {0,-3} | {1,-20} | {2,-15} | {3,-17} | {4,-15} | {5,-20}", "Id", "Nome do Equipamento", "Preço", "Data de Fabricação", "Número de Série", "Fabricante");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                for (int i = 0; i < listaIdEquipamentos.Count; i++)
                {
                    DateTime dataModificada = (DateTime)listaDataEquipamentos[i];
                    Console.WriteLine(" {0,-3} | {1,-20} | {2,-15} | {3,-18} | {4,-15} | {5,-20} ", listaIdEquipamentos[i], listaNomeEquipamentos[i], listaPrecoEquipamentos[i], dataFabricacaoEquipamento.ToString("dd/MM/yyyy"), listaNumeroDeSerieEquipamentos[i], listaFabricanteEquipamentos[i]);
                }
            }
            Console.ReadKey();
        }

        static void MostraSucessoAoUsuarioEquipamentos(string mensagem)
        {
            Console.WriteLine("____________________________________________________________________________");
            Console.WriteLine($" Id : {idEquipamentos}\n Nome do Equipamento: {nomeEquipamento}\n Preço do Equipamento: {precoEquipamento}\n Data da fabricação: {dataFabricacaoEquipamento.ToString("dd/MM/yyyy")}\n Número de série: {numeroDeSerieEquipamento}\n Fabricante do Equipamento: {fabricanteEquipamento}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mensagem);
            Console.ResetColor();
            Console.WriteLine("____________________________________________________________________________");
        }

        static void AdicionaNasListasEquipamentos()
        {
            idEquipamentos++;
            listaIdEquipamentos.Add(idEquipamentos);
            listaNomeEquipamentos.Add(nomeEquipamento);
            listaPrecoEquipamentos.Add(precoEquipamento);
            listaDataEquipamentos.Add(dataFabricacaoEquipamento);
            listaNumeroDeSerieEquipamentos.Add(numeroDeSerieEquipamento);
            listaFabricanteEquipamentos.Add(fabricanteEquipamento);
        }

        static void PegadadosDoUsuarioEquipamentos()
        {
            Console.Write("Nome do Equipamento: ");
            nomeEquipamento = Console.ReadLine();
            Console.Write("Preço: ");
            precoEquipamento = Convert.ToDouble(Console.ReadLine());
            Console.Write("Numero de Serie: ");
            numeroDeSerieEquipamento = Console.ReadLine();
            Console.Write("Data de fabricação: ");
            dataFabricacaoEquipamento = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Fabricante: ");
            fabricanteEquipamento = Console.ReadLine();
        }

        #endregion

        #region Chamados
        static void PegadadosDoUsuarioChamados()
        {
            Console.Write("Titulo do Chamado: ");
            títuloChamados = Console.ReadLine();
            Console.Write("Descricao: ");
            descriçãoChamados = Console.ReadLine();
            Console.Write("Id do Equipamento: ");
            EquipamentosIdChamados = Convert.ToInt32(Console.ReadLine());
            Console.Write("Data de Abertura: ");
            dataDeAberturaChamados = Convert.ToDateTime(Console.ReadLine());
        }

        static void AdicinaChamados(string resposta)
        {
            while (resposta.ToUpper() != "S")
            {
                MostraTodosOsEquipamentos();
                PegadadosDoUsuarioChamados();
                AdicionaNasListasChamados();
                MostraSucessoAoUsuarioChamados("Adicionado com Sucesso!");
                Console.Write("Sair: ");
                resposta = Console.ReadLine();
            }
        }
        static void AdicionaNasListasChamados()
        {
            idChamados++;
            listaIdChamados.Add(idChamados);
            listaTitulosChamados.Add(títuloChamados);
            listaIdDoEquipamentoNosChamados.Add(EquipamentosIdChamados);
            listaDataAberturaChamados.Add(dataDeAberturaChamados);
            listaDescricaoChamados.Add(descriçãoChamados);
        }
        static void MostraSucessoAoUsuarioChamados(string mensagem)
        {
            string nomeEquipamentoDoChamado = "";
            for (int i = 0; i < listaIdEquipamentos.Count; i++)
            {
                if (listaIdEquipamentos[i].Equals(EquipamentosIdChamados))
                {
                    nomeEquipamentoDoChamado = (string)listaNomeEquipamentos[i];
                }
            }
            Console.WriteLine("____________________________________________________________________________");
            Console.WriteLine($"Id : {idChamados}, Título: {títuloChamados}, Nome do Equipamento: {nomeEquipamentoDoChamado}, data da abertura: {dataDeAberturaChamados.ToString("dd/MM/yyyy")}, Descrição: {descriçãoChamados}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mensagem);
            Console.ResetColor();
            Console.WriteLine("____________________________________________________________________________");
        }
        static void MostraTodosOsChamados()
        {
            string nomeEquipamentoDoChamado = "";
            if (listaIdChamados.Count == 0)
            {
                MensagemDeErro("Nao existem valores na lista");
            }
            else
            {
                Console.WriteLine(" {0,-3} | {1,-24} | {2,-21} |  {3,-20} |  {4,-50} | {5,-20}", "Id", "Título", "Equipamento", "Data de Abertura", "Descrição", "Dias Abertos");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------");
                for (int i = 0; i < listaIdChamados.Count; i++)
                {
                    for (int j = 0; j < listaIdEquipamentos.Count; j++)
                    {
                        if (listaIdEquipamentos[j].Equals(listaIdDoEquipamentoNosChamados[i]))
                        {
                            nomeEquipamentoDoChamado = (string)listaNomeEquipamentos[j];
                        }
                    }
                    TimeSpan diasAbertos = new TimeSpan();
                    DateTime dataModificada = (DateTime)listaDataAberturaChamados[i];
                    diasAbertos = DateTime.UtcNow - dataModificada;
                    Console.WriteLine(" {0,-3} | {1,-24} | {2,-21} |  {3,-20} |  {4,-50} | {5,-20}", listaIdChamados[i], listaTitulosChamados[i], nomeEquipamentoDoChamado, dataModificada.ToString("dd / MM / yyyy"), listaDescricaoChamados[i], diasAbertos.Days);
                }

            }

            Console.ReadKey();
        }

        static void ModificaUmChamadoEscolhido(string resposta)
        {
            while (resposta.ToUpper() != "S")
            {
                if (listaIdChamados.Count == 0)
                {
                    MensagemDeErro("Nao existem valores na lista");
                }
                else
                {
                    MostraTodosOsChamados();
                    Console.WriteLine("Id de quem deseja modificar;");
                    int idParaModificar = Convert.ToInt32(Console.ReadLine());
                    for (int i = 0; i < listaIdChamados.Count; i++)
                    {
                        if (listaIdChamados[i].Equals(idParaModificar))
                        {
                            PegadadosDoUsuarioChamados();
                            ModificaAsListasChamados(i);
                            MostraSucessoAoUsuarioChamados("Modificado com Sucesso!");
                        }
                        else
                        {
                            MensagemDeErro("Id Inválido");
                        }
                    }
                }
                Console.Write("Sair: ");
                resposta = Console.ReadLine();
            }
        }
        static void ModificaAsListasChamados(int i)
        {
            listaTitulosChamados[i] = títuloChamados;
            listaIdDoEquipamentoNosChamados[i] = EquipamentosIdChamados;
            listaDataAberturaChamados[i] = dataDeAberturaChamados;
            listaDescricaoChamados[i] = descriçãoChamados;
        }
        static void DeletaChamados(string resposta)
        {
            while (resposta.ToUpper() != "S")
            {
                if (listaIdChamados.Count == 0)
                {
                    MensagemDeErro("Nao existem valores na lista");
                }
                else
                {
                    MostraTodosOsChamados();
                    Console.WriteLine("Id de quem deseja deletar;");
                    int idParaDeletar = Convert.ToInt32(Console.ReadLine());
                    for (int i = 0; i < listaIdChamados.Count; i++)
                    {
                        if (listaIdChamados[i].Equals(idParaDeletar))
                        {
                            RemoveDasListasChamados(i);
                        }
                        else
                        {
                            MensagemDeErro("Id Inválido");
                        }
                    }


                }
                Console.Write("Sair: ");
                resposta = Console.ReadLine();
            }
        }

        static void RemoveDasListasChamados(int i)
        {
            listaIdChamados.RemoveAt(i);
            listaTitulosChamados.RemoveAt(i);
            listaIdDoEquipamentoNosChamados.RemoveAt(i);
            listaDataAberturaChamados.RemoveAt(i);
            listaDescricaoChamados.RemoveAt(i);
        }
        static string MostraMenuChamados()
        {
            string resposta;
            Console.WriteLine("Menu Chamados: ");
            Console.WriteLine(" 1- Adicionar Chamados\n 2- Visualizar Todos os Chamados\n 3- Editar Chamados\n 4- Excluir Chamados\n S para Sair");
            resposta = Console.ReadLine();
            return resposta;
        }
        static string CRUD_Chamados(string resposta)
        {
            while (resposta.ToUpper() != "S")
            {
                Console.Clear();
                resposta = MostraMenuChamados();
                if (resposta == "1")
                {
                    Console.Clear();
                    AdicinaChamados(resposta);
                    resposta = "";
                    continue;
                }
                if (resposta == "2")
                {
                    Console.Clear();
                    MostraTodosOsChamados();
                    resposta = "";
                    continue;
                }
                if (resposta == "3")
                {
                    Console.Clear();
                    ModificaUmChamadoEscolhido(resposta);
                    resposta = "";
                    continue;
                }
                if (resposta == "4")
                {
                    Console.Clear();
                    DeletaChamados(resposta);
                    resposta = "";
                    continue;
                }


            }

            return resposta;
        }


        #endregion
    }
}

