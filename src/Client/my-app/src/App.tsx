// Copyright (c) 2025 - Jun Dev. All rights reserved

import "./App.css";
import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { PageLayout, SidebarMenu } from "./components/layout";
import { TaskLayout } from "./pages/task";
import { setCredentials, useAuth } from "./features";
import AuthPage from "./pages/auth/AuthPage";
import { Toaster } from "./components/common";
import { authApi } from "./api/services";
import { setToken } from "./api/client/tokenUtils";

function App() {
  const dispatch = useDispatch();
  const { isAuthenticated } = useAuth();
  useEffect(() => {
    if (isAuthenticated) return;

    authApi.refresh()?.then(res => {
      if (!res || !res.data) return;

      const { userId, email, firstName, lastName, roles, accessToken, refreshToken } = res.data;
      setToken(accessToken, refreshToken);
      dispatch(setCredentials({ auth: { userId, email, firstName, lastName, roles }}));
    });
  }, [dispatch]);
  return (
    <>
      <Toaster position="top-right" />
      {!isAuthenticated ? (
        <AuthPage />
      ) : (
        <PageLayout>
          <PageLayout.Header>
            <div className="w-full flex justify-between items-center">
              <h1 className="text-xl font-bold">Task Management System</h1>
            </div>
          </PageLayout.Header>

          <PageLayout.Sidebar>
            <SidebarMenu />
          </PageLayout.Sidebar>

          <PageLayout.Body>
            <TaskLayout />
          </PageLayout.Body>
        </PageLayout>
      )}
    </>
  );
}

export default App;
