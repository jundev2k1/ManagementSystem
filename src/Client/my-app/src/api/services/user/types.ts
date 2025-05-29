// Copyright (c) 2025 - Jun Dev. All rights reserved

import type { Entity } from "../../../common/types";

export interface UserModel extends Entity {
  userId: string | null;
  userName: string;
  email: string;
  avatar: string;
  firstName: string;
  lastName: string;
  fullName: string;
  phoneNumber: string;
  address: string;
  validFlg: string;
}
