// Copyright (c) 2025 - Jun Dev. All rights reserved

export const handleError = async (res: Response): Promise<Error> => {
  if (res.status === 401) {
    // Optional: redirect to login
    window.location.href = '/login';
    return new Error('Unauthorized');
  }

  const message = await res.text();
  return new Error(message || `Request failed with status ${res.status}`);
};
