import { useState } from 'react';
import axios from 'axios';
import { z } from  'zod';
import { zodResolver } from '@hookform/resolvers/zod';
import { useForm } from "react-hook-form"

import { Label } from '@/components/ui/label'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select"
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormMessage,
} from "@/components/ui/form"
import { Link } from 'react-router-dom';


function Register() {
  const [roles, setRoles] = useState(["Nurse", "Scheduler", "Administrator"])

  const formSchema = z.object({
    firstName : z.string().min(1, "First name is required"),
    lastName : z.string().min(1, "Last name is required"),
    email: z.string().email("Invalid email address"),
    password : z.string().min(6, {
      message: "Password must contain 6 or more characters."
    }).max(50, {
      message: "Password must be less than 50 characters long."
    }),
    role: z.string().min(1, "Role is required")
  })

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      firstName: "",
      lastName: "",
      email: "",
      password: "",
      role: "",
    },
  })

  function onSubmit(values: z.infer<typeof formSchema>) {
    console.log(values)

    // TODO: Implement registration logic
  }

  return (
    <>
      <div className='flex flex-col items-center justify-center min-h-screen'>
        <div className='flex flex-col gap-7 max-w-sm'>
            <header className='space-y-1'>
                <h2>Create account</h2>
                <p className="text-sm text-slate-500">Welcome to Scribble! Complete registration to start Scribbling.</p>
            </header>
            <Form {...form}>
              <form onSubmit={form.handleSubmit(onSubmit)} className='flex flex-col gap-7'>
                <div className='flex flex-col gap-3.5'>

                  <div className='flex flex-row gap-3.5'>
                    <FormField
                      control={form.control}
                      name="firstName"
                      render={({ field }) => (
                          <FormItem>
                            <Label>First Name</Label>
                            <FormControl>
                              <Input placeholder='First name' {...field} />
                            </FormControl>
                            <FormMessage />
                          </FormItem>
                        )}
                    />
                    <FormField
                      control={form.control}
                      name="lastName"
                      render={({ field }) => (
                          <FormItem>
                            <Label>Last Name</Label>
                            <FormControl>
                              <Input placeholder='Last name' {...field} />
                            </FormControl>
                            <FormMessage />
                          </FormItem>
                        )}
                      />
                  </div>

                  <FormField
                    control={form.control}
                    name="email"
                    render={({ field }) => (
                        <FormItem>
                          <Label>Email</Label>
                          <FormControl>
                            <Input placeholder='Email' {...field} />
                          </FormControl>
                          <FormMessage />
                        </FormItem>
                      )}
                    />

                  <FormField
                    control={form.control}
                    name="password"
                    render={({ field }) => (
                        <FormItem>
                          <Label>Password</Label>
                          <FormControl>
                            <Input type='password' placeholder='Password' {...field} />
                          </FormControl>
                          {form.formState.errors.password ? (
                            <FormMessage />
                          ) : (
                            <FormDescription>Password must contain 6 or more characters.</FormDescription>
                          )}
                        </FormItem>
                      )}
                    />

                    <FormField
                      control={form.control}
                      name="role"
                      render={({ field }) => (
                          <FormItem>
                            <Label>Role</Label>
                            <Select onValueChange={field.onChange}>
                              <FormControl>
                                <SelectTrigger>
                                  <SelectValue placeholder='Select your role' />
                                </SelectTrigger>
                              </FormControl>
                              <SelectContent>
                                {roles.map((role) =>
                                  <SelectItem key={role} value={role}>{role}</SelectItem>
                                )}
                              </SelectContent>
                            </Select>
                            <FormMessage />
                          </FormItem>
                        )}
                      />
                    </div>

                    <div className='flex flex-col gap-3.5'>
                      <Button type="submit">Create account</Button>
                      <Button type='button' variant="outline" asChild>
                        <Link to='/'>Already have an account?</Link>
                      </Button>
                    </div>

              </form>
            </Form>
        </div>
      </div>
    </>
  )
}

export { Register }