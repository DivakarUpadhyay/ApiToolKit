using ApiToolKit.Domain.Entities.Cloud;

namespace ApiToolKit.Domain.Interfaces.Repositories.Cloud;

public interface ISpeechRepository
{
    Task SaveRecognitionAsync(SpeechRecognition recognition);
    Task<IEnumerable<SpeechRecognition>> GetByProjectIdAsync(string projectId);
}
