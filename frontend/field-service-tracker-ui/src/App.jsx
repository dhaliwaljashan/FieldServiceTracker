import { useEffect, useState } from "react";
import {
  createServiceRequest,
  deleteServiceRequest,
  getServiceRequests,
  patchServiceRequestStatus,
  updateServiceRequest,
} from "./api/serviceRequestApi";

import Login from "./components/Login";
import ServiceRequestForm from "./components/ServiceRequestForm";
import ServiceRequestList from "./components/ServiceRequestList";
import FilterBar from "./components/FilterBar";
import LoadingSpinner from "./components/LoadingSpinner";
import "./index.css";

function App() {
  const [user, setUser] = useState(() => {
    const savedUser = localStorage.getItem("user");
    return savedUser ? JSON.parse(savedUser) : null;
  });

  const [requests, setRequests] = useState([]);
  const [selectedRequest, setSelectedRequest] = useState(null);
  const [status, setStatus] = useState("");
  const [priority, setPriority] = useState("");
  const [searchTerm, setSearchTerm] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const [successMessage, setSuccessMessage] = useState("");

  const loadRequests = async () => {
    try {
      setLoading(true);
      setError("");

      const data = await getServiceRequests(status, priority);
      setRequests(data);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (user) {
      loadRequests();
    }
  }, [status, priority, user]);

  useEffect(() => {
    if (successMessage) {
      const timer = setTimeout(() => {
        setSuccessMessage("");
      }, 3000);

      return () => clearTimeout(timer);
    }
  }, [successMessage]);

  const handleSubmit = async (formData) => {
    try {
      setError("");
      setSuccessMessage("");

      if (selectedRequest) {
        await updateServiceRequest(selectedRequest.id, {
          ...formData,
          status: selectedRequest.status,
        });

        setSelectedRequest(null);
        setSuccessMessage("Service request updated successfully.");
      } else {
        await createServiceRequest(formData);
        setSuccessMessage("Service request created successfully.");
      }

      await loadRequests();
    } catch (err) {
      setError(err.message);
    }
  };

  const handleDelete = async (id) => {
    const confirmed = window.confirm(
      "Are you sure you want to delete this service request?"
    );

    if (!confirmed) return;

    try {
      setError("");
      setSuccessMessage("");

      await deleteServiceRequest(id);
      setSuccessMessage("Service request deleted successfully.");

      await loadRequests();
    } catch (err) {
      setError(err.message);
    }
  };

  const handleStatusChange = async (id, newStatus) => {
    try {
      setError("");
      setSuccessMessage("");

      await patchServiceRequestStatus(id, newStatus);
      setSuccessMessage("Status updated successfully.");

      await loadRequests();
    } catch (err) {
      setError(err.message);
    }
  };

  const handleEdit = (request) => {
    setSuccessMessage("");
    setError("");
    setSelectedRequest(request);
  };

  const handleCancelEdit = () => {
    setSelectedRequest(null);
    setSuccessMessage("");
    setError("");
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    setUser(null);
    setRequests([]);
    setSelectedRequest(null);
    setError("");
    setSuccessMessage("");
  };

  const clearFilters = () => {
    setStatus("");
    setPriority("");
    setSearchTerm("");
    setSuccessMessage("");
    setError("");
  };

  const filteredRequests = requests.filter((request) => {
    const search = searchTerm.toLowerCase();

    return (
      request.ticketNumber.toLowerCase().includes(search) ||
      request.customerName.toLowerCase().includes(search) ||
      request.location.toLowerCase().includes(search) ||
      request.assignedTechnician?.toLowerCase().includes(search)
    );
  });

  const openCount = requests.filter((x) => x.status === "Open").length;
  const inProgressCount = requests.filter((x) => x.status === "In Progress").length;
  const resolvedCount = requests.filter((x) => x.status === "Resolved").length;
  const criticalCount = requests.filter((x) => x.priority === "Critical").length;

  if (!user) {
    return <Login onLogin={setUser} />;
  }

  return (
    <div className="app-container">
      <header className="app-header">
        <div className="header-left">
          <h1>FieldServiceTracker</h1>
          <p>Field Operations Service Request Management</p>
        </div>

        <div className="header-right">
          <div className="user-info">
            <span className="user-name">{user.fullName}</span>
            <span className="user-role">{user.role}</span>
          </div>

          <button className="logout-btn" onClick={handleLogout}>
            Logout
          </button>
        </div>
      </header>

      {error && <div className="error-box">{error}</div>}

      {successMessage && <div className="success-box">{successMessage}</div>}

      <ServiceRequestForm
        key={selectedRequest?.id || "new"}
        selectedRequest={selectedRequest}
        onSubmit={handleSubmit}
        onCancelEdit={handleCancelEdit}
      />

      <div className="dashboard-grid">
        <div className="dashboard-card">
          <h3>{openCount}</h3>
          <p>Open Requests</p>
        </div>

        <div className="dashboard-card">
          <h3>{inProgressCount}</h3>
          <p>In Progress</p>
        </div>

        <div className="dashboard-card">
          <h3>{resolvedCount}</h3>
          <p>Resolved</p>
        </div>

        <div className="dashboard-card">
          <h3>{criticalCount}</h3>
          <p>Critical Priority</p>
        </div>
      </div>

      <FilterBar
        status={status}
        priority={priority}
        searchTerm={searchTerm}
        onStatusChange={setStatus}
        onPriorityChange={setPriority}
        onSearchChange={setSearchTerm}
        onClear={clearFilters}
      />

      {loading ? (
        <LoadingSpinner />
      ) : (
        <ServiceRequestList
          requests={filteredRequests}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onStatusChange={handleStatusChange}
        />
      )}
    </div>
  );
}

export default App;