import { useState, useEffect } from "react";

const initialFormData = {
  customerName: "",
  location: "",
  issueDescription: "",
  priority: "Medium",
  assignedTechnician: "",
};

const ServiceRequestForm = ({ onSubmit, selectedRequest, onCancelEdit }) => {
  const [formData, setFormData] = useState(selectedRequest || initialFormData);
  const [errors, setErrors] = useState({});

  useEffect(() => {
    if (selectedRequest) {
      setFormData(selectedRequest);
    } else {
      setFormData(initialFormData);
    }

    setErrors({});
  }, [selectedRequest]);

  const validateForm = () => {
    const newErrors = {};

    if (!formData.customerName.trim()) {
      newErrors.customerName = "Customer Name is required.";
    }

    if (!formData.location.trim()) {
      newErrors.location = "Location is required.";
    }

    if (!formData.issueDescription.trim()) {
      newErrors.issueDescription = "Issue Description is required.";
    }

    return newErrors;
  };

  const handleChange = (e) => {
    const { name, value } = e.target;

    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));

    // Clear only the error for the field user is typing in
    setErrors((prevErrors) => ({
      ...prevErrors,
      [name]: "",
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    const validationErrors = validateForm();

    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors);
      return;
    }

    onSubmit(formData);
    setFormData(initialFormData);
    setErrors({});
  };

  return (
    <form className="service-form" onSubmit={handleSubmit}>
      <h2>{selectedRequest ? "Update Service Request" : "Create Service Request"}</h2>

      <label htmlFor="customerName">Customer Name *</label>
      <input
        name="customerName"
        placeholder="Customer Name"
        value={formData.customerName}
        onChange={handleChange}
      />
      {errors.customerName && (
        <span className="field-error">{errors.customerName}</span>
      )}

      <label htmlFor="location">Location *</label>
      <input
        name="location"
        placeholder="Location"
        value={formData.location}
        onChange={handleChange}
      />
      {errors.location && (
        <span className="field-error">{errors.location}</span>
      )}

      <label htmlFor="issueDescription">Issue Description *</label>
      <textarea
        name="issueDescription"
        placeholder="Issue Description"
        value={formData.issueDescription}
        onChange={handleChange}
      />
      {errors.issueDescription && (
        <span className="field-error">{errors.issueDescription}</span>
      )}

      <label htmlFor="priority">Priority</label>
      <select
        name="priority"
        value={formData.priority}
        onChange={handleChange}
      >
        <option value="Low">Low</option>
        <option value="Medium">Medium</option>
        <option value="High">High</option>
        <option value="Critical">Critical</option>
      </select>

      <label htmlFor="assignedTechnician">Assigned Technician</label>
      <input
        name="assignedTechnician"
        placeholder="Assigned Technician"
        value={formData.assignedTechnician || ""}
        onChange={handleChange}
      />

      <div className="form-actions">
        <button type="submit">
          {selectedRequest ? "Update Request" : "Create Request"}
        </button>

        {selectedRequest && (
          <button type="button" onClick={onCancelEdit}>
            Cancel
          </button>
        )}
      </div>
    </form>
  );
};

export default ServiceRequestForm;