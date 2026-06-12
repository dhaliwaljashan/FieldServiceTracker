const FilterBar = ({
  status,
  priority,
  searchTerm,
  onStatusChange,
  onPriorityChange,
  onSearchChange,
  onClear,
}) => {
  return (
    <div className="filter-bar">
      <input
        type="text"
        placeholder="Search by ticket, customer, location, or technician"
        value={searchTerm}
        onChange={(e) => onSearchChange(e.target.value)}
      />

      <select value={status} onChange={(e) => onStatusChange(e.target.value)}>
        <option value="">All Statuses</option>
        <option value="Open">Open</option>
        <option value="In Progress">In Progress</option>
        <option value="Resolved">Resolved</option>
        <option value="Closed">Closed</option>
      </select>

      <select
        value={priority}
        onChange={(e) => onPriorityChange(e.target.value)}>
        <option value="">All Priorities</option>
        <option value="Low">Low</option>
        <option value="Medium">Medium</option>
        <option value="High">High</option>
        <option value="Critical">Critical</option>
      </select>

      <button type="button" className="secondary-btn" onClick={onClear}>
        Clear Filters
      </button>
    </div>
  );
};

export default FilterBar;