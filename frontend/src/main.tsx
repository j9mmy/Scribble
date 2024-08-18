import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { Login } from './pages/Login.tsx'
import { Register } from './pages/Register.tsx'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import './index.css'

const router = createBrowserRouter([
  {
    path: '/',
    element: <Login />
  },
  {
    path: '/register',
    element: <Register />
  }
]);

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>,
)
