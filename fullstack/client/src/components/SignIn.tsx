import React, {useState} from 'react';
import "./styles.css";
import logo from "../assets/logo.png"
import {useAtom} from "jotai";
import {AdminAtom, JwtAtom} from "../atoms/atoms.ts";
import toast from "react-hot-toast";
import {randomUid} from "./App.tsx";
import {Link, useNavigate} from "react-router";
import {DashboardRoute, ForgotPasswordRoute} from "../helpers/routeConstants.tsx";
import {authClient, userClient} from "../apiControllerClients.ts";

const SignIn = () => {
    const [ ,setJwt] = useAtom(JwtAtom);
    const [,setAdmin] = useAtom(AdminAtom);

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    return (
        <div className="bg-background-black flex items-center justify-center min-h-screen">
            <div className="bg-background-grey border border-white/10 rounded-2xl p-10 max-w-lg">
                <div className="flex flex-col items-center mb-8 w-2/3 mx-auto">
                   <img
                        src={logo}
                        alt="logo"
                   />
                </div>

                <h2 className="text-center text-white text-xl mb-6">Sign In</h2>

                <div className="space-y-6">
                    <div>
                        <label className="block mb-2 text-sm text-text-grey" htmlFor="email">Email Address</label>
                        <input
                            type="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            id="email"
                            className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                        />
                    </div>

                    <div>
                        <label className="block mb-2 text-sm text-text-grey" htmlFor="password">Password</label>
                        <div>
                        <input
                            type="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            id="password"
                            className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                        />
                        </div>
                    </div>

                    <div>
                        <Link
                            to={ForgotPasswordRoute}
                            className="text-sm underline text-[--color-text-baby-blue]"
                        >
                            Forgot password?
                        </Link>
                    </div>

                    <button
                        onClick={() => authClient.login({email: email, password: password, clientId: randomUid}).then(r => {
                            const tempJwt = r.jwt;
                            userClient.getUserByEmail(email).then(r => {
                                if (r.roleName == "Admin") {
                                    toast.success("You have been signed in");
                                    localStorage.setItem("jwt", tempJwt);
                                    localStorage.setItem("firstname", r.firstName!);
                                    localStorage.setItem("lastname", r.lastName!);
                                    localStorage.setItem("email", r.email!);
                                    setJwt(tempJwt);
                                    setAdmin(r);
                                    navigate(DashboardRoute);
                                }
                                else {
                                    toast.error("Your acc is not an Admin account");
                                }
                            })
                        }).catch(() => {
                            toast.error("Username or password is incorrect");
                        })}
                        type="submit"
                        className="w-full py-3 rounded-md text-[var(--color-background-black)] bg-[--color-button-grey] hover:bg-blue-500 hover:text-white"
                    >
                        Sign In
                    </button>
                </div>
            </div>
        </div>
    );
};

export default SignIn;
