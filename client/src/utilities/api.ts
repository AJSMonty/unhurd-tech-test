import axios from 'axios';

export const initApi = (baseUrl: string) => {
  axios.defaults.baseURL = baseUrl;
};

export const initToken = (token?: string) => {
  if (token) {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  } else {
    axios.defaults.headers.common['Authorization'] = '';
  }
};
