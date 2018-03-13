using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculadoraCompleta.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            // preparar os primeiros valores a aparecer no visor
            ViewBag.Visor = "0";
            Session["operador"] = "";
            Session["limpaVisor"] = true;
            return View();
        }

		// GET: Home
		[HttpPost]
		public ActionResult Index(string bt,string visor)
		{
            //determinar a acao a executar
            switch (bt){
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                // recupera o resultado sobre a limpeza do visor 
                    bool limpaEcra = (bool)Session["limpaVisor"];
                    if (limpaEcra || visor.Equals("0")) visor = bt;
                    else visor += bt;
                    //marcar o visor para continuar a escrita do operando
                    Session["limpaVisor"] = false;
                    break;
                case "+/-": visor = Convert.ToDouble(visor) * -1 +""; break;
                case ",": if (!visor.Contains(",")) visor += ",";break;
                case "+":
                case "-":
                case "x":
                case ":":
                    //se nao é a primeira vez que pressiono o valor
                    if (((string)Session["operador"]).Equals(""))
                    {
                        //preservar os valores do VISOR usando variaveis de sessao
                        Session["primeiroOperando"] = visor;
                        //guardar valor do operador
                        Session["operador"] = bt;
                        //preparar o visor para uma nova introduçao
                        visor = "";
                    }
                    else
                    {
                        //agora e que se vai fazer a conta
                        //obter os operados
                        double primeiroOperando = Convert.ToDouble((string)Session["primeiroOperando"]);
                        double segundoOperado = Convert.ToDouble(visor);
                        // escolher a operaçao a fazer com operador anterior
                        switch ((string)Session["operador"])
                        {
                            case "+": visor = primeiroOperando + segundoOperado + ""; break;
                            case "-": visor = primeiroOperando - segundoOperado + ""; break;
                            case "x": visor = primeiroOperando * segundoOperado + ""; break;
                            case ":": visor = primeiroOperando / segundoOperado + ""; break;
                        }// ((string)Session["operador"])
                    }
                   
                    //preservar os valores fornecidos para operaçoes futuras
                    if (bt.Equals("=")) { Session["operador"] = ""; }
                    else Session["operador"] = bt;
                        Session["primeiroOperando"] = visor;
                        //marcar o visor para "limpeza"
                        Session["limpaVisor"] = true;break;
                case "C":
                    //limpar a calculadora fazendo um reset total
                    visor = "0";
                    Session["operador"] = "";
                    Session["limpaVisor"] = true;

                    break;
            }
            //enviar o resultado para a view
            ViewBag.Visor = visor;
                return View();
		}

	}
}