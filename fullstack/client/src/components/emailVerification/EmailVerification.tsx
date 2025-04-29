import React from "react";
import "../styles.css";

const EmailVerification = () => {



    return(
        <div className="bg-background-black flex items-center justify-center min-h-screen">
            <div className="flex items-center justify-center min-h-screen">
                <div className="bg-background-grey border border-white/10 rounded-2xl p-10 w-full max-w-lg">
                    <h2 className="text-center text-white text-xl mb-6">Email Verification</h2>
                    <p className="text-center text-text-grey mb-4">Please verify your email address to continue.</p>
                    <button className="w-full py-3 rounded-md text-[var(--color-background-black)] bg-[var(--color-button-grey)] hover:bg-blue-500 hover:text-white">
                        Confirm Email Address
                    </button>
                </div>
            </div>

        </div>
    );
}

export default EmailVerification;
