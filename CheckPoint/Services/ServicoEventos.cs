using CheckPoint.Dto;
using CheckPoint.Model;
using MongoDB.Driver;

namespace CheckPoint.Services
{
    public class ServicoEventos
    {
        private readonly IMongoCollection<Evento> _collectionEventos;

        public ServicoEventos(IMongoCollection<Evento> colecaoEvento)
        {
            _collectionEventos = colecaoEvento;
        }

        public async Task<List<Evento>> GetEventosAsync() =>
            await _collectionEventos.Find(_ => true).ToListAsync();

        public async Task<Evento?> GetEventoIdAsync(string id) =>
            await _collectionEventos.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Evento> CreateAsync(EventoDto dto)
        {
            var evento = new Evento
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Data = dto.Data,
                Local = dto.Local,
                Categoria = dto.Categoria,
                CapacidadeMaxima = dto.CapacidadeMaxima,
                DataCriacao = dto.DataCriacao,
            };

            await _collectionEventos.InsertOneAsync(evento);
            return evento;
        }

        public async Task<bool> UpdateAsync(string id, Evento eventoUpdated)
        {
            var result = await _collectionEventos.ReplaceOneAsync(x => x.Id == id, eventoUpdated);
            return result.MatchedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collectionEventos.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<List<Evento>> GetHalfAsync(int pagina, int qtd)
        {
            return await _collectionEventos
               .Find(_ => true)
               .Skip((pagina - 1) * qtd)
               .Limit(qtd)
               .ToListAsync();
        }

        public async Task<long> CountAsync()
        {
            return await _collectionEventos.CountDocumentsAsync(_ => true);
        }
    }
}
