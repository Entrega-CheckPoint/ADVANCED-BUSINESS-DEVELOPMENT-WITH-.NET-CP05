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

        public async Task<Evento> CreateAsync(Evento evento)
        {
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
    }
}
