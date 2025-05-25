import { useEffect } from 'react';
import './App.css'
import { useAuth } from './features/auth/useAuth'

function App() {
  const { login } = useAuth();

  useEffect(() => {
    debugger
    login("admin02", "123456789");
  }, [])
  return (
    <>
      Hello world
    </>
  )
}

export default App
