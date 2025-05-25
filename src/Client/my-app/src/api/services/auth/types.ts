// Copyright (c) 2025 - Jun Dev. All rights reserved

export type AuthRequest = {
  userName: string;
  password: string;
}

export interface AuthResponse {
  userId: string;
  useName: string;
  email: string;
  firstName: string;
  lastName: string;
  roles: string[];
  token: string;
}

export interface RegisterRequest {
	userName: string;
	email: string;
	password: string;
	firstName: string;
	lastName: string;
	phoneNumber: string;
	address: string;
}
