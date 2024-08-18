import { useState } from 'react'
import axios from 'axios';
import { Button, buttonVariants } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { Link } from 'react-router-dom';

function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const loginSubmit = async () => {
    console.log('Login attempt \nEmail:', email, '\nPassword:', password);

    axios.post('api/user/login', {
      email: email,
      password: password
    })
    .then(response => {
      console.log(response.data);
    })
    .catch(error => {
      console.error(error);
    });
  }

  const logoutSubmit = async () => {
    axios.post('api/user/logout')
    .then(response => {
      console.log(response.data);
    })
    .catch(error => {
      console.error(error);
    });
  }

  return (
    <>
      <div className='flex flex-col items-center justify-center min-h-screen'>
        <div className='space-y-6 max-w-screen-sm'>
            <header>
                <h1>Scribble</h1>
                <p className="text-sm text-slate-500">Welcome back! Please login to start Scribbling.</p>
            </header>
            <div className='space-y-3'>
                <div className='space-y-1'>
                    <Label htmlFor="email">Email</Label>
                    <Input onChange={e => setEmail(e.target.value)} placeholder="Email"/>
                </div>
                <div className='space-y-1'>
                    <Label htmlFor="password">Password</Label>
                    <Input onChange={e => setPassword(e.target.value)} type="password" placeholder="Password" />
                </div>
            </div>
            <footer className='flex flex-col space-y-3 justify-between'>
                <Button onClick={loginSubmit}>Login</Button>
                <Button variant='outline' asChild>
                  <Link to='/register'>Create a new account</Link>
                </Button>
                <Button onClick={logoutSubmit} variant="link">Password forgotten?</Button>
            </footer>
        </div>
      </div>
    </>
  )
}

export { Login }