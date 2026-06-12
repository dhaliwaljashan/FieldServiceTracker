const ServiceRequestList = ({ requests, onEdit, onDelete, onStatusChange }) => {
  if (requests.length === 0) {
    return (
      <p className="empty-message">
        No service requests match the selected criteria. Try adjusting your
        filters.
      </p>
    );
  }

  return (
    <div className="table-wrapper">
      <table>
        <thead>
          <tr>
            <th>Ticket</th>
            <th>Customer Name</th>
            <th>Location</th>
            <th>Issue Description</th>
            <th>Priority</th>
            <th>Assigned Technician</th>
            <th>Status</th>
            <th>Created</th>
            <th>Actions</th>
          </tr>
        </thead>

        <tbody>
          {requests.map((request) => (
            <tr key={request.id}>
              <td className="ticket-column">{request.ticketNumber}</td>
              <td>{request.customerName}</td>
              <td>{request.location}</td>
              <td>{request.issueDescription}</td>
              <td>{request.priority}</td>
              <td>{request.assignedTechnician || "Unassigned"}</td>
              <td>
                <select
                  value={request.status}
                  onChange={(e) => onStatusChange(request.id, e.target.value)}>
                  <option value="Open">Open</option>
                  <option value="In Progress">In Progress</option>
                  <option value="Resolved">Resolved</option>
                  <option value="Closed">Closed</option>
                </select>
              </td>
              <td>{new Date(request.createdAt).toLocaleDateString()}</td>
              <td className="action-buttons">
                <button
                 onClick={() => onEdit(request)}
                >
                  Edit
                </button>
                <button
                  className="danger-btn"
                  onClick={() => onDelete(request.id)}
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ServiceRequestList;