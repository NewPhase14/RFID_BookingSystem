import React, { useEffect, useState } from "react";
import { authClient } from "../../apiControllerClients.ts";
import { SignInRoute } from "../../helpers/routeConstants.tsx";
import { useNavigate } from "react-router";

const ForgotPassword = () => {
    const [email, setEmail] = useState<string>("");

    const [errorMessage, setErrorMessage] = useState<string>("");
    const [successMessage, setSuccessMessage] = useState<string>("");

    const [loading, setLoading] = useState<boolean>(false);

    const [emailSent, setEmailSent] = useState<boolean>(false);

    const navigate = useNavigate();

    useEffect(() => {
        if (successMessage) {
            setErrorMessage("");
        }
    }, [successMessage]);

    const handleForgotPassword = async (e: React.FormEvent) => {
        e.preventDefault();

        if (!email) {
            setErrorMessage("Please enter your email.");
            return;
        }

        setLoading(true);

        try {
            await authClient.forgotPassword({ email });
            setSuccessMessage("An email with a password reset link has been sent to you.");
            setEmailSent(true);
            setTimeout(() => {
                navigate(SignInRoute);
            }, 4000);
        } catch (error) {
            setErrorMessage("An error occurred. Please try again.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="bg-background-black flex items-center justify-center min-h-screen px-4">
            <div className="bg-background-grey border border-white/10 rounded-2xl p-10 w-full max-w-md shadow-lg">
                <h2 className="text-center text-white text-2xl font-semibold mb-8">
                    Reset Password
                </h2>

                <p className="text-center text-text-grey mb-8">
                    Enter your email address below and we will send you a link to reset your password.
                </p>

                <form className="space-y-6" onSubmit={handleForgotPassword}>
                    <div>
                        <label className="block mb-2 text-sm font-medium text-white">Email</label>
                        <input
                            type="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            placeholder="Enter your email"
                            className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30 transition"
                        />
                    </div>

                    {errorMessage && (
                        <div className="text-red-500 text-sm -mt-4 mb-4">
                            <p>{errorMessage}</p>
                        </div>
                    )}

                    {successMessage && (
                        <div className="text-green-500 text-sm -mt-4 mb-4">
                            <p>{successMessage}</p>
                        </div>
                    )}

                    <button
                        type="submit"
                        disabled={loading || emailSent}
                        className={`w-full py-3 rounded-md text-[var(--color-background-black)] ${
                            loading || emailSent
                                ? "bg-gray-500 cursor-not-allowed"
                                : "bg-[var(--color-button-grey)] hover:bg-blue-500 hover:text-white transition"
                        }`}
                    >
                        {loading ? "Loading..." :  "Send Password Reset Email"}
                    </button>
                </form>
            </div>
        </div>
    );
};

export default ForgotPassword;
