import { useState } from 'react'
import axios from 'axios';
import { Login } from '@/Login'

function App() {
  const [forecasts, setForecasts] = useState([]);

  const fetchForecasts = async () => {
    axios.get('/api/weatherforecast')
      .then(response => {
        setForecasts(response.data);
        console.log(response.data);
      })
      .catch(error => {
        console.error(error);
      });
  }

  return (
    <>
      <header className="App-header">
        <h1>Welcome to Vite + React</h1>
      </header>
      <body>
        <Login />
      </body>
      <footer>
        <p>footer...</p>
      </footer>
    </>
  )
}

export default App
