import { AuthBindings } from "@refinedev/core";
import { axiosInstance } from "@refinedev/simple-rest";
import { apiManager } from "./core/api.manager";

export const TOKEN_KEY = "refine-auth";

export const authProvider: AuthBindings = {
  login: async ({ username, password }) => {
    if (username && password) {
      let loginResult = await apiManager.auth.loginCreate({
        username,
        password,
      });
      if (loginResult.status == 200) {
        localStorage.setItem(TOKEN_KEY, loginResult.data.token!);
        return {
          success: true,
          redirectTo: "/",
        };
      }
    }

    return {
      success: false,
      error: {
        name: "LoginError",
        message: "Invalid username or password",
      },
    };
  },
  register: async ({ username, password, email, passwordConfirm }) => {
    if (password != passwordConfirm) {
      return {
        success: false,
        error: {
          name: "Password Error",
          message: "Passwords Doesn't Match!",
        },
      };
    }

    if (username && email && password) {
      let result = await apiManager.auth.registerCreate({
        username,
        password,
        email,
      });
      if (result.status == 200) {
        return {
          success: true,
          redirectTo: "/",
        };
      }

      return {
        success: false,
        error: {
          name: "Register Error",
          message: JSON.stringify(result.data),
        },
      };
    }

    return { success: false };
  },
  logout: async () => {
    localStorage.removeItem(TOKEN_KEY);
    return {
      success: true,
      redirectTo: "/login",
    };
  },
  check: async () => {
    const token = localStorage.getItem(TOKEN_KEY);
    if (token) {
      axiosInstance.defaults.headers.common = {
        Authorization: `Bearer ${token}`,
      };

      return {
        authenticated: true,
      };
    }

    return {
      authenticated: false,
      redirectTo: "/login",
    };
  },
  getPermissions: async () => null,
  getIdentity: async () => {
    const token = localStorage.getItem(TOKEN_KEY);
    if (token) {
      let userResult = await fetch("http://localhost:5079/auth/me", {
        method: "GET",
        headers: {
          Accept: "*/*",
          "User-Agent": "Mozilla",
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      });

      if (userResult.status !== 200) {
        throw new Error("There is an error about user");
      }

      return userResult;
    }
    return null;
  },
  onError: async (error) => {
    console.error(error);
    return { error };
  },
};
