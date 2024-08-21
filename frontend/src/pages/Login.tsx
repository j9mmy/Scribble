import { useState } from 'react'
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { z } from 'zod';
import { zodResolver } from '@hookform/resolvers/zod';

import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert"
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage,
} from "@/components/ui/form"
import { Loader2, AlertCircle } from 'lucide-react';

function Login() {
  const [isLoading, setIsLoading] = useState(false)
  const [loginStatus, setLoginStatus] = useState(0)
  const navigate = useNavigate()

  const formSchema = z.object({
    email: z.string().email("Valid email is required"),
    password : z.string().min(1, "Password is required")
  })

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      email: "",
      password: "",
    },
  })

  function onSubmit(values: z.infer<typeof formSchema>) {
    setIsLoading(true)
    setLoginStatus(0)

    axios.post('api/user/login', {
      email: values.email,
      password: values.password,
    })
    .then(response => {
      console.log(response.data);
      navigate('/dashboard')
      logoutSubmit(); // Temporary ofcourse
    })
    .catch(error => {
      console.error(error);
      setLoginStatus(error.response.status)
    })
    .finally(() => setIsLoading(false));
  }


  const logoutSubmit = async () => {
    console.log('Logging current user out...')

    axios.post('api/user/logout')
    .then(response => {
      console.log(response.data);
      navigate('/')
    })
    .catch(error => {
      console.error(error);
    });
  }

  return (
    <>
      <div className='flex flex-col items-center justify-center min-h-screen'>
        <div className='space-y-6 max-w-sm w-full mx-4'>

          <header>
              <h1>Scribble</h1>
              <p className="text-sm text-muted-foreground">Welcome back! Please login to start Scribbling.</p>
          </header>

          <Form {...form}>
            <form className='flex flex-col gap-8' onSubmit={form.handleSubmit(onSubmit)}>

              <div className='flex flex-col gap-4'>
                <FormField
                  control={form.control}
                  name='email'
                  render={({field}) => (
                    <FormItem className='flex flex-col gap-1'>
                      <Label>Email</Label>
                      <FormControl>
                        <Input type='email' placeholder='Email' {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />

                <FormField
                  control={form.control}
                  name='password'
                  render={({field}) => (
                    <FormItem className='flex flex-col gap-1'>
                      <Label>Password</Label>
                      <FormControl>
                        <Input type='password' placeholder='Password' {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
              </div>

              <div className='flex flex-col gap-3 justify-between'>
                <Button type='submit' disabled={isLoading} onClick={() => onSubmit}>
                  {isLoading && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
                  Login
                </Button>
                <Button type='button' variant='outline' asChild>
                  <Link to='/register'>Create a new account</Link>
                </Button>
                <div className='flex justify-center items-center h-10'>
                  <Button type='button' variant="link" className='w-fit h-fit p-0'>
                    <Link to='/password'>Password forgotten?</Link>
                  </Button>
                </div>
              </div>
              
              {loginStatus >= 400 && loginStatus < 500 && (
                <Alert variant={'destructive'}>
                  <AlertCircle className='h-4 w-4' />
                  <AlertTitle>Login failed</AlertTitle>
                  <AlertDescription>
                    {loginStatus == 404 && (
                      'No user found with this email'
                    )}
                    {loginStatus == 401 && (
                      'Incorrect email and/or password combination'
                    )}
                  </AlertDescription>
                </Alert>
              )}

            </form>
          </Form>
        </div>
      </div>
    </>
  )
}

export { Login }