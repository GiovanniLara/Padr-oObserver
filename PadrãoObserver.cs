using System;
using System.Collections.Generic;

namespace Observer
{
    public interface IObserverCarrinho
    {
        void Atualizar(string item, string acao);
    }


    public class Carrinho
    {
        private List<IObserverCarrinho> _observadores = new List<IObserverCarrinho>();
        private List<string> _itens = new List<string>();

        
        public void AdicionarObservador(IObserverCarrinho observador)
        {
            _observadores.Add(observador);
        }

        
        public void RemoverObservador(IObserverCarrinho observador)
        {
            _observadores.Remove(observador);
        }

       
        public void AdicionarItem(string item)
        {
            _itens.Add(item);
            NotificarObservadores(item, "adicionado");
        }

        
        public void RemoverItem(string item)
        {
            if (_itens.Remove(item)) 
            {
                NotificarObservadores(item, "removido");
            }
            else
            {
                Console.WriteLine($"Item '{item}' não encontrado no carrinho.");
            }
        }

      
        private void NotificarObservadores(string item, string acao)
        {
            foreach (var observador in _observadores)
            {
                observador.Atualizar(item, acao);
            }
        }
    }

    
    public class EstoqueObserver : IObserverCarrinho
    {
        public void Atualizar(string item, string acao)
        {
            Console.WriteLine($"Estoque atualizado: Item '{item}' foi {acao} no carrinho.");
        }
    }

  
    public class PagamentoObserver : IObserverCarrinho
    {
        public void Atualizar(string item, string acao)
        {
            Console.WriteLine($"Pagamento atualizado: Item '{item}' foi {acao} no carrinho.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Carrinho carrinho = new Carrinho();

            EstoqueObserver estoqueObserver = new EstoqueObserver();
            PagamentoObserver pagamentoObserver = new PagamentoObserver();
            carrinho.AdicionarObservador(estoqueObserver);
            carrinho.AdicionarObservador(pagamentoObserver);

            
            carrinho.AdicionarItem("Laptop");
            
            carrinho.RemoverItem("Laptop");
          
            carrinho.RemoverItem("Tablet");
        }
    }
}

