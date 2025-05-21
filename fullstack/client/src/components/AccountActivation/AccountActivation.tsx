import React, { useEffect, useState } from "react";
import "../styles.css";
import { AiFillEye, AiFillEyeInvisible } from "react-icons/ai";
import { authClient } from "../../apiControllerClients.ts";
import { SignInRoute } from "../../helpers/routeConstants.tsx";
import {useNavigate} from "react-router";

const AccountActivation = () => {
    const [token, setToken] = useState<string>("");

    const [showPassword, setShowPassword] = useState(false);
    const [showConfirmPassword, setShowConfirmPassword] = useState(false);

    const [password, setPassword] = useState<string>("");
    const [confirmPassword, setConfirmPassword] = useState<string>("");

    const [errorMessage, setErrorMessage] = useState("");
    const [successMessage, setSuccessMessage] = useState("");

    const [isActivated, setIsActivated] = useState<boolean>(false);

    const [loading, setLoading] = useState<boolean>(false);

    const navigate = useNavigate();

    const passwordRequirements = {
        minLength: /.{8,}/,
        uppercase: /[A-Z]/,
        lowercase: /[a-z]/,
        number: /[0-9]/,
        specialChar: /[!"#$%&'()*+,-./:;<=>?@[\]^_`{|}~]/,
    };

    useEffect(() => {
        const params = new URLSearchParams(window.location.search);
        const urlToken = params.get("token");

        if (urlToken) {
            setToken(urlToken);
        } else {
            setErrorMessage("Error: Token is missing in the URL.");
            setIsActivated(false);
        }
    }, []);

    useEffect(() => {
        if (isActivated) {
            setErrorMessage("");
        }
    }, [isActivated]);

    const validatePassword = () => {
        if (password !== confirmPassword) {
            return "Passwords do not match.";
        }

        if (!passwordRequirements.minLength.test(password)) {
            return "Password must be at least 8 characters long.";
        }

        if (!passwordRequirements.uppercase.test(password)) {
            return "Password must contain at least one uppercase letter.";
        }

        if (!passwordRequirements.lowercase.test(password)) {
            return "Password must contain at least one lowercase letter.";
        }

        if (!passwordRequirements.number.test(password)) {
            return "Password must contain at least one number.";
        }

        if (!passwordRequirements.specialChar.test(password)) {
            return "Password must contain at least one special character.";
        }

        return null;
    };


    const handleActivateAccount = async (e: React.FormEvent) => {
        e.preventDefault();

        const validationError = validatePassword();
        if (validationError) {
            setErrorMessage(validationError);
            return;
        }

        setLoading(true);

        try {
            await authClient.accountActivation({ tokenId: token, password });
            setSuccessMessage("Account activated successfully!");
            setIsActivated(true);

            setTimeout(() => {
                navigate(SignInRoute);
            }, 5000);

        } catch (error) {
            setErrorMessage("Error activating account. This link may have expired.");
            setIsActivated(false);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="bg-background-black flex items-center justify-center min-h-screen">
            <div className="flex items-center justify-center min-h-screen">
                <div className="bg-background-grey border border-white/10 rounded-2xl p-10 w-full max-w-lg">
                    <h2 className="text-center text-white text-xl mb-6">Activate Account</h2>
                    <p className="text-center text-text-grey mb-4">
                        To activate your account, please set a new password.
                    </p>

                    <form className="space-y-6" onSubmit={handleActivateAccount}>
                        {/* Password Field */}
                        <div className="relative">
                            <input
                                type={showPassword ? "text" : "password"}
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                placeholder="New Password"
                                className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                            />
                            <div
                                className="absolute top-4 right-4 cursor-pointer"
                                onClick={() => setShowPassword(!showPassword)}
                            >
                                {showPassword ? <AiFillEyeInvisible color="white" /> : <AiFillEye color="white" />}
                            </div>
                        </div>

                        {/* Confirm Password Field */}
                        <div className="relative">
                            <input
                                type={showConfirmPassword ? "text" : "password"}
                                value={confirmPassword}
                                onChange={(e) => setConfirmPassword(e.target.value)}
                                placeholder="Confirm New Password"
                                className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                            />
                            <div
                                className="absolute top-4 right-4 cursor-pointer"
                                onClick={() => setShowConfirmPassword(!showConfirmPassword)}
                            >
                                {showConfirmPassword ? <AiFillEyeInvisible color="white" /> : <AiFillEye color="white" />}
                            </div>
                        </div>

                        {errorMessage && (
                            <div className="mb-6 text-red-500">
                                <p>{errorMessage}</p>
                            </div>
                        )}

                        {successMessage && (
                            <div className="mb-6 text-green-500">
                                <p>{successMessage}</p>
                            </div>
                        )}

                        <button
                            type="submit"
                            disabled={loading}
                            className={`w-full py-3 rounded-md text-[var(--color-background-black)] ${
                                loading
                                    ? "bg-gray-500 cursor-not-allowed"
                                    : "bg-[var(--color-button-grey)] hover:bg-blue-500 hover:text-white"
                            }`}
                        >
                            {loading ? "Activating..." : "Activate Account"}
                        </button>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default AccountActivation;
