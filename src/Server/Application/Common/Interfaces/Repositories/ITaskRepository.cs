using Application.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories;

public interface ITaskRepository
{
	Task<TaskInfo?> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken = default);

	Task<IEnumerable<TaskInfo>> GetTasksByCriteria(CancellationToken cancellationToken = default);

	Task<Guid> CreateNewTaskAsync(TaskInfo task, CancellationToken cancellationToken = default);

	Task UpdateTaskAsync(TaskInfo task, CancellationToken cancellationToken = default);

	Task DeleteTaskAsync(Guid id, CancellationToken cancellationToken = default);
}
