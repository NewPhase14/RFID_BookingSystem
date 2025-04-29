import React from 'react';
import "../styles.css";
import logo from "../../assets/logo.png"
const Login = () => {
    return (
        <div className="bg-background-black flex items-center justify-center min-h-screen">
            <div className="bg-background-grey border border-white/10 rounded-2xl p-10 w-full max-w-lg">
                <div className="flex flex-col items-center mb-8">
                   <img
                        src={logo}
                        alt="logo"
                   />
                </div>

                <h2 className="text-center text-white text-xl mb-6">Sign In</h2>

                <form className="space-y-6">
                    <div>
                        <label className="block mb-2 text-sm text-text-grey" htmlFor="email">Email Address</label>
                        <input
                            type="email"
                            id="email"
                            className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                        />
                    </div>

                    <div>
                        <label className="block mb-2 text-sm text-text-grey" htmlFor="password">Password</label>
                        <div>
                        <input
                            type="password"
                            id="password"
                            className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                        />
                        </div>
                    </div>

                    <div>
                        <a href={"gg"} className="text-sm underline text-[var(--color-text-baby-blue)]">Forgot password?</a>
                    </div>

                    <button
                        type="submit"
                        className="w-full py-3 rounded-md text-[var(--color-background-black)] bg-[var(--color-button-grey)] hover:bg-blue-500 hover:text-white"
                    >
                        Sign In
                    </button>
                </form>
            </div>
        </div>
    );
};

export default Login;
