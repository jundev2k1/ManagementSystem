// Copyright (c) 2025 - Jun Dev. All rights reserved

import './App.css'
import { useAuth } from './features'
import AuthPage from './pages/auth/AuthPage';

function App() {
  const { isAuthenticated } = useAuth();

  if (!isAuthenticated) return <AuthPage />

  return (
    <>
      Hello world
    </>
  )
}

export default App
