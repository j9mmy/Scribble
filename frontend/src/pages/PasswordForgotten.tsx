import { Label } from "@/components/ui/label"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import {
    Form,
    FormControl,
    FormDescription,
    FormField,
    FormItem,
    FormMessage,
  } from "@/components/ui/form"

import { Link } from "react-router-dom"
import { useState } from "react"

function PasswordForgotten() {
    const [email, setEmail] = useState('')

    const submitPasswordRetrieval = async () => {
        console.log('Entered email: ',  email);
        
        // TODO:    Submit request to retrieve password.
        //          Would be nice to confirm that the entered email does actually have an account.
    }

    return (
        <>
            <div className='flex flex-col items-center justify-center min-h-screen'>
                <div className='space-y-6 max-w-sm w-full mx-4'>
                    <header>
                        <h2>Password forgotten?</h2>
                        <p className="text-sm text-slate-500">Enter your email to resume using Scribble.</p>
                    </header>
                    <div className='space-y-3'>
                        <div className='space-y-1'>
                            <Label htmlFor="email">Email</Label>
                            <Input onChange={e => setEmail(e.target.value)} placeholder="Email"/>
                        </div>
                    </div>
                    <footer className='flex flex-col space-y-3 justify-between'>
                        <Button onClick={submitPasswordRetrieval}>Send confirmation email</Button>
                        <Button variant='outline' asChild>
                            <Link to='/'>Cancel</Link>
                        </Button>
                    </footer>
                </div>
            </div>
        </>
    )

}

export { PasswordForgotten }