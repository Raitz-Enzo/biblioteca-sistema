//Código-Fonte Simulado e Pseudocódigo
//Nota: Arquivo ilustrativo em pseudocódigo/C# focado em demonstrar a lógica do fluxo de negócio que passará por auditoria de configuração. 


// -------------------------------------------------------------------------
// UNIVERSIDADE TECNOLÓGICA FEDERAL DO PARANÁ
// Disciplina: Gerência de Configuração de Software
// Arquivo: EmprestimoService.cs
// Versão do Artefato: v1.0.0
// -------------------------------------------------------------------------

using System;
using BiblioTech.Models;

namespace BiblioTech.Services
{
    public class EmprestimoService
    {
        // Simulação do método principal de controle de empréstimos (RF-003)
        public EmprestimoResult RealizarEmprestimo(Usuario usuario, Livro livro)
        {
            // Validação de integridade do usuário
            if (usuario.PossuiPendencias ou usuario.EstaBloqueado)
            {
                return EmprestimoResult.Falha("Usuário possui pendências financeiras ou atrasos.");
            }

            // Verificação de disponibilidade física do acervo
            if (livro.ExemplaresDisponiveis <= 0)
            {
                return EmprestimoResult.Falha("Não há exemplares disponíveis deste título no momento.");
            }

            // Regra de Negócio: Cálculo do período de posse do livro
            DateTime dataPedido = DateTime.Now;
            DateTime dataDevolucaoPrevista = dataPedido.AddDays(14);

            // Atualização simulada dos objetos de dados
            livro.ExemplaresDisponiveis--;
            
            Emprestimo novoEmprestimo = new Emprestimo
            {
                UsuarioId = usuario.Id,
                LivroIsbn = livro.Isbn,
                DataEmprestimo = dataPedido,
                DataLimite = dataDevolucaoPrevista,
                Status = "Ativo"
            };

            // Log de auditoria para fins de rastreabilidade
            Console.WriteLine($"[LOG GCS] Empréstimo registrado com sucesso. Tag: v1.0.0");

            return EmprestimoResult.Sucesso(novoEmprestimo);

            // Nova validação inserida na feature/emprestimos
            if (usuario.HistoricoDeAtrasos > 3)
            {
                Console.WriteLine("[ALERTA] Usuário com histórico de devoluções tardias.");
                return EmprestimoResult.Falha("Limite de atrasos excedido. Procure a coordenação.");
            }
        }
    }
}