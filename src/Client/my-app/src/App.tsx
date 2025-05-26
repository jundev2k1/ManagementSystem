// Copyright (c) 2025 - Jun Dev. All rights reserved

import "./App.css";
import { PageLayout, SidebarMenu } from "./components/layout";
import { TaskLayout } from "./pages/task";
import { useAuth } from "./features";
import AuthPage from "./pages/auth/AuthPage";
import { Toaster } from "./components/common";

function App() {
  const { isAuthenticated } = useAuth();
  return (
    <>
      <Toaster position="top-right" />
      {!isAuthenticated ? (
        <AuthPage />
      ) : (
        <PageLayout>
          <PageLayout.Header>
            <h1>Task Management</h1>
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
