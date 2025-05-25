// Copyright (c) 2025 - Jun Dev. All rights reserved

import { configureStore } from '@reduxjs/toolkit';
import { authReducer } from '../features';

export const store = configureStore({
  reducer: {
    auth: authReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;