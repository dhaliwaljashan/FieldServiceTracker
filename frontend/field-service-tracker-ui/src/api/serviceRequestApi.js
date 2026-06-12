import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7098/api",
  timeout: 10000,
  headers: {
    "Content-Type": "application/json",
  },
});

// Attach JWT token to every protected request after login
api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

const handleError = (error, isLoginRequest = false) => {
  if (error.code === "ECONNABORTED") {
    throw new Error("Request timed out. Please try again.");
  }

  if (!error.response) {
    throw new Error(
      "Unable to connect to the backend API. Please make sure the server is running."
    );
  }

  if (error.response.status === 401) {
    if (!isLoginRequest) {
      localStorage.removeItem("token");
      localStorage.removeItem("user");
    }

    throw new Error(
      isLoginRequest
        ? "Invalid email or password."
        : "Your session has expired. Please login again."
    );
  }

  if (error.response.status === 403) {
    throw new Error("Access denied. You do not have permission to perform this action.");
  }

  if (error.response.status >= 500) {
    throw new Error("Server error occurred. Please try again later.");
  }

  if (error.response?.data?.error) {
    throw new Error(error.response.data.error);
  }

  if (error.response?.data?.errors) {
    const errors = Object.values(error.response.data.errors).flat().join(", ");
    throw new Error(errors);
  }

  throw new Error("Unable to process your request. Please try again.");
};

export const login = async (email, password) => {
  try {
    const response = await api.post("/Auth/login", { email, password });
    return response.data;
  } catch (error) {
    handleError(error, true);
  }
};

export const getServiceRequests = async (status, priority) => {
  try {
    const response = await api.get("/ServiceRequests", {
      params: { status, priority },
    });
    return response.data;
  } catch (error) {
    handleError(error);
  }
};

export const createServiceRequest = async (requestData) => {
  try {
    const response = await api.post("/ServiceRequests", requestData);
    return response.data;
  } catch (error) {
    handleError(error);
  }
};

export const updateServiceRequest = async (id, requestData) => {
  try {
    const response = await api.put(`/ServiceRequests/${id}`, requestData);
    return response.data;
  } catch (error) {
    handleError(error);
  }
};

export const patchServiceRequestStatus = async (id, status) => {
  try {
    const response = await api.patch(`/ServiceRequests/${id}/status`, { status });
    return response.data;
  } catch (error) {
    handleError(error);
  }
};

export const deleteServiceRequest = async (id) => {
  try {
    await api.delete(`/ServiceRequests/${id}`);
  } catch (error) {
    handleError(error);
  }
};